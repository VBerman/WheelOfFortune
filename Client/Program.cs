using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WheelOfFortune.Client;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WheelOfFortune.Client.Providers;
using WheelOfFortune.Client.Services;
using System.Reflection;
using WheelOfFortune.Shared.Model.RealEstate;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<RealEstateService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(RealEstateMappingProfile)));

await builder.Build().RunAsync();