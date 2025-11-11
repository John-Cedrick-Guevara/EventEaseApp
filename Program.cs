using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EventEaseApp;
using EventEaseApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register EventService as a singleton for state management across the app
builder.Services.AddSingleton<EventService>();

// Register UserSessionService as scoped - each user gets their own session instance
builder.Services.AddScoped<UserSessionService>();

await builder.Build().RunAsync();
