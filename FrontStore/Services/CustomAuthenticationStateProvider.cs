using Blazored.LocalStorage;
using FrontStore.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontStore.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        public ILocalStorageService _localStorageService { get; set; }

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task Login(AuthResponse authResponse)
        {
            await _localStorageService.SetItemAsync("user", authResponse.User);
            await _localStorageService.SetItemAsync("token", authResponse.Token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("user");
            await _localStorageService.RemoveItemAsync("token");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>("token");

            if (String.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // deserialize the token, extract the claims and build a ClaimsPrincipal
            var claimsPrincipal = BuildClaimsPrincipal(token);

            return new AuthenticationState(claimsPrincipal);

        }

        private ClaimsPrincipal BuildClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenS = tokenHandler.ReadJwtToken(token);

            var claims = tokenS.Claims.Select(c => new Claim(c.Type, c.Value)).ToList();
            var identity = new ClaimsIdentity(claims, "jwt");

            return new ClaimsPrincipal(identity);
        }
    }
}
