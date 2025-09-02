using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
// using BlazorStateApp;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

using front;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<UsersStateService>();
builder.Services.AddSingleton<ProductsStateService>();
builder.Services.AddHttpClient<TokenService>();

// Adding Blazored LocalStorage and SessionStorage services to manage client-side storage.
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();
