using FrontStore.Model;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace FrontStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;


        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, CustomAuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }


        public async Task SignIn(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", request);
            var content = await response.Content.ReadAsStringAsync();
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(content);
            _authenticationStateProvider.Login(authResponse);

        }

        public Task SignUp(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
