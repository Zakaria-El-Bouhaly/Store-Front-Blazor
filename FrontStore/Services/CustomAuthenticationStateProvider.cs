using Blazored.LocalStorage;
using FrontStore.Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontStore.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthResponse _authResponse;
        public string Token { get; private set; }

        public void Login(AuthResponse authResponse)
        {
            _authResponse = authResponse;
            Token = authResponse.Token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            _authResponse = null;
            Token = null;

            // add claims to context



            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            if (_authResponse == null || string.IsNullOrWhiteSpace(Token))
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }

            // deserialize the token, extract the claims and build a ClaimsPrincipal
            var claimsPrincipal = BuildClaimsPrincipal(_authResponse.Token);

            return Task.FromResult(new AuthenticationState(claimsPrincipal));

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
