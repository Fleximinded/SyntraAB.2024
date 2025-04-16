using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Syntra.Auth0.Demo.Client.Pages;
using Syntra.Auth0.Demo.Components;

namespace Syntra.Auth0.Demo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuth0WebAppAuthentication(options =>
        {
            options.ClientId = builder.Configuration["Auth0:ClientId"] ?? "";
            options.Domain = builder.Configuration["Auth0:Domain"] ?? "";
        });
        builder.Services.AddCascadingAuthenticationState();
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents()
            .AddAuthenticationStateSerialization();
      
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);


        app.MapGet(
            "/Account/login",
            async (HttpContext context,string returnUrl = "/") =>
            {
                var authProps = new LoginAuthenticationPropertiesBuilder().WithRedirectUri(returnUrl).Build();
                await context.ChallengeAsync(Auth0Constants.AuthenticationScheme,authProps);
            }
        );
        app.MapGet(
            "/Account/logout",
            async (HttpContext context) =>
            {
                var authProps = new LogoutAuthenticationPropertiesBuilder().WithRedirectUri("/").Build();
                await context.SignOutAsync(Auth0Constants.AuthenticationScheme, authProps);
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        );




        app.Run();
    }
}
