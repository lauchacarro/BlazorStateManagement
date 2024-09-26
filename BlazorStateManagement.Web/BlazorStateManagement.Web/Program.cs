using BlazorStateManagement.Api;
using BlazorStateManagement.Api.Routes;
using BlazorStateManagement.Store;
using BlazorStateManagement.Web.Components;

using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;

using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Blazor
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7252") });


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddFluxor(options =>
    options.ScanAssemblies(typeof(TodoState).Assembly)
    .UseReduxDevTools()
    .UseRouting());


// WebApi   
builder.Services.AddWebApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

   
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// WebApi
app.MapAppApi();


// Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorStateManagement.Web.Client._Imports).Assembly);

app.Run();
