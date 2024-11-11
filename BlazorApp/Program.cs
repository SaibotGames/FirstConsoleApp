using BlazorApp.Auth;
using BlazorApp.Components;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler()
    {
        // Disable SSL certificate validation for local development (use cautiously)
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    // Return a HttpClient with the custom handler and base address
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5144/")  // Use your actual API URL
    };
});

builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();
builder.Services.AddScoped<IPostService, HttpPostService>();
builder.Services.AddScoped<IUserService, HttpUserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();