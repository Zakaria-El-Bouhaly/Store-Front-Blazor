using FrontStore.Model;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json;
using Blazored.LocalStorage;


namespace FrontStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;


        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;

        }

        public async Task SignIn(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", request);
            var content = await response.Content.ReadAsStringAsync();
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(content);
            await _localStorage.SetItemAsync("authToken", authResponse.Token);
            await _localStorage.SetItemAsync("user", authResponse.User);


        }

        public Task SignUp(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
