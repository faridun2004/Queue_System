using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Queue_UI;
using Queue_UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var serverURL = new Uri("https://localhost:7221");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = serverURL });
builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = serverURL);
builder.Services.AddScoped<IApointmentService, ApointmentService>();
builder.Services.AddScoped<IHttpAPIProvider, HttpAPIProvider>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddScoped<UserService>();

builder.Services.AddBlazoredLocalStorageAsSingleton();
await builder.Build().RunAsync();
