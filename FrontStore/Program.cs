using FrontStore;
using FrontStore.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using CurrieTechnologies.Razor.SweetAlert2;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<AuthorizationMessageHandler>();



builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress =
    new Uri(builder.Configuration["BaseUrl"])

    )
    .AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("ServerAPI"));


//builder.Services.AddScoped(
//    sp => new HttpClient
//    {
//        BaseAddress = new Uri(builder.Configuration["BaseUrl"])
//    });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


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
builder.Services.AddSweetAlert2(); ;

await builder.Build().RunAsync();
