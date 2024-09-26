using BlazorStateManagement.Store;

using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFluentUIComponents();

builder.Services.AddFluxor(options =>
    options.ScanAssemblies(typeof(TodoState).Assembly)
    .UseReduxDevTools()
    .UseRouting());

await builder.Build().RunAsync();
