using Microsoft.EntityFrameworkCore;
using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Database.Context;
using Syntra.Frituurtje.Database.Repository;
using Syntra.Frituurtje.Wasm.Client.Pages;
using Syntra.Frituurtje.Wasm.Client.Services;
using Syntra.Frituurtje.Wasm.Components;
using Syntra.Frituurtje.Wasm.Services;
using Syntra.Frituurtje.Wasm.Shared;

namespace Syntra.Frituurtje.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FrituurtjeContext>(c => c.UseSqlServer(connectionstring));

            builder.Services.AddBlazorBootstrap();

            builder.Services.AddScoped<IMenuRepository,MenuRepository>();
            builder.Services.AddScoped<IMenuService, MenuService>();

            builder.Services.AddControllers();
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

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.MapControllers();
            await app.RunAsync();
        }
    }
}
