using FrontStore;
using FrontStore.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddHttpClient<ICategoryService, CategoryService>(
    client => client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));

builder.Services.AddHttpClient<IProductService, ProductService>(
    client => client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));


builder.Services.AddHttpClient<IAuthService, AuthService>(
       client => client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));



builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped(provider => new JwtMiddleware(provider.GetRequiredService<CustomAuthenticationStateProvider>()));

await builder.Build().RunAsync();
