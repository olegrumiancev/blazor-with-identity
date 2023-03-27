using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1.Server.Data;
using Project1.Server.Models;
using Project1.Shared;
using System.ComponentModel;

namespace Project1.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AuthorizeController(
            UserManager<Person> userManager,
            SignInManager<Person> signInManager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(parameters.UserName);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterParameters parameters)
        {
            var user = new Person();
            user.UserName = parameters.UserName;
            try
            {
                var result = await _userManager.CreateAsync(user, parameters.Password);
                if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

                // add a user role
                //var person = _dbContext.Users.FirstOrDefault(p => p.UserName == UserName);
                await _userManager.AddToRoleAsync(user, AppRoles.User);

                return await Login(new LoginParameters
                {
                    UserName = parameters.UserName,
                    Password = parameters.Password
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public PersonDto UserInfo()
        {
            return BuildUserInfo();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserRole(PersonRoleDto personRoleDto)
        {
            if (personRoleDto == null)
            {
                return BadRequest("Person info is null");
            }

            try
            {
                var person = _dbContext.Users.First(p => p.UserName == personRoleDto.UserName);

                var id = await _userManager.RemoveFromRolesAsync(person, new[] { AppRoles.User, AppRoles.Admin });
                var id2 = await _userManager.AddToRoleAsync(person, personRoleDto.RoleName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
          
            
            //await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserInfo(PersonDto personDto)
        {
            if (personDto == null)
            {
                return BadRequest("Person info is null");
            }

            var person = _dbContext.Users.First(p => p.UserName == personDto.UserName);
            personDto.CopyProperties(person);
            _dbContext.Users.Update(person);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<List<PersonRoleDto>> UserRole()
        {
            var result = new List<PersonRoleDto>();
            var p = _dbContext.Users.ToList() ?? new();
            foreach (var person in p)
            {
                var roles = await _userManager.GetRolesAsync(person);
                result.Add(new() { UserName = person.UserName, RoleName = roles.FirstOrDefault() });
            }
            return result;
        }


        private PersonDto BuildUserInfo()
        {
            // query database
            var person = _dbContext.Users.FirstOrDefault(p => p.UserName == User.Identity.Name);

            var personDto = new PersonDto
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                ExposedClaims = User.Claims
                    //Optionally: filter the claims you want to expose to the client
                    //.Where(c => c.Type == "test-claim")
                    //.Where(c => c.Type.Contains("role") is false)
                    //.ToDictionary(c => c.Type, c => c.Value)
                    .Select(c => $"{c.Type}||{c.Value}").ToList()
            };

            person.CopyProperties(personDto);
            return personDto;
        }
    }
}
