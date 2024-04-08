using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;

namespace FrontStore.Services
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthorizationMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
         HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request is being sent {0}", JsonConvert.SerializeObject(request));

            var token = await _localStorage.GetItemAsync<string>("token");

            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", "Bearer " + token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
