using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FrontStore.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService _localStorageService;


        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "user") }.Concat(new[] { new Claim("token", token) });
                var identity = new ClaimsIdentity(claims, "apiauth");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }

        }
    }
}
