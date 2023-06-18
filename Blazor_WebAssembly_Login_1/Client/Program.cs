using Blazor_WebAssembly_Login_1.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Blazor_WebAssembly_Login_1.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// Services
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationExtension>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
