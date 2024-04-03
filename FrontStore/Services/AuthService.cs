using FrontStore.Model;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;


namespace FrontStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;


        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, CustomAuthenticationStateProvider authenticationStateProvider
            , NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }


        public async Task SignIn(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", request);
            var content = await response.Content.ReadAsStringAsync();
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(content);
            await _authenticationStateProvider.Login(authResponse);
            _navigationManager.NavigateTo("/");

        }

        public async Task SignUp(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", request);
            _navigationManager.NavigateTo("/login");

        }

        public async Task SignOut()
        {
           await _authenticationStateProvider.Logout();
            _navigationManager.NavigateTo("/login");
        }
    }
}
