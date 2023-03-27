using Project1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Client.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task Login(LoginParameters loginParameters);
        Task Register(RegisterParameters registerParameters);
        Task Logout();
        Task<PersonDto> GetUserInfo();
        Task<bool> SetUserInfo(PersonDto personDto);
        Task<bool> SetUserRole(PersonRoleDto personRoleDto);
        Task<List<PersonRoleDto>> GetUserRoleList();
    }
}
