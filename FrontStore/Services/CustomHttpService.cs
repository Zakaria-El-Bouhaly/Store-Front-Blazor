using Blazored.LocalStorage;

namespace FrontStore.Services
{
    public class CustomHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly string _token;

        public CustomHttpService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;                        
        }
        

        
    }
}
