using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace FrontStore.Services
{


    public class JwtMiddleware : DelegatingHandler
    {
        
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

        public JwtMiddleware(CustomAuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("JwtMiddleware");
            var token = _authenticationStateProvider.Token;
            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}