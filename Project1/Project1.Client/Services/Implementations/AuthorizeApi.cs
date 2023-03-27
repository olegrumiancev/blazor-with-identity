using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Project1.Client.Services.Contracts;
using Project1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project1.Client.Services.Implementations
{
    public class AuthorizeApi : IAuthorizeApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            //var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync("api/Authorize/Login", loginParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            //var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync("api/Authorize/Register", registerParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<PersonDto> GetUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<PersonDto>("api/Authorize/UserInfo");
            return result;
        }

        public async Task<bool> SetUserInfo(PersonDto personDto)
        {
            var resultMessage = await _httpClient.PostAsJsonAsync("api/Authorize/UserInfo", personDto);
            return resultMessage.IsSuccessStatusCode;
        }

        public async Task<bool> SetUserRole(PersonRoleDto personRoleDto)
        {
            var resultMessage = await _httpClient.PostAsJsonAsync("api/Authorize/UserRole", personRoleDto);
            return resultMessage.IsSuccessStatusCode;
        }

        public async Task<List<PersonRoleDto>> GetUserRoleList()
        {
            var resultMessage = await _httpClient.GetFromJsonAsync<List<PersonRoleDto>>("api/Authorize/UserRole");
            return resultMessage ?? new();
        }
    }
}
