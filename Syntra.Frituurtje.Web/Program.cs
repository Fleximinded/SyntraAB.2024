using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Database.Context;
using Syntra.Frituurtje.Database.Repository;
using Syntra.Frituurtje.Web.Components;
using Syntra.Frituurtje.Web.Define;
using Syntra.Frituurtje.Web.Services;

namespace Syntra.Frituurtje.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
         //   await EnsureDockerContainerIsRunning();

            var builder = WebApplication.CreateBuilder(args);
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddBlazorBootstrap();

            var connectionStr = config.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FrituurtjeContext>(o=> o.UseSqlServer(connectionStr));
            var runmode = config.GetValue<string>("RunMode");
           if(runmode == "Test")
            {
                builder.Services.AddScoped<IMenuRepository, MenuRepositoryTest>();
            }
            else
            {
                builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            }
            builder.Services.AddScoped<IMenuService, MenuService>();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if(!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }


        private static async Task EnsureDockerContainerIsRunning()
        {
            using var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();

            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });

            var container = containers.Where(c => c.Names.Contains("/Frituurtje")).FirstOrDefault();

            if(container == null)
            {
                // Create and start the container
                await client.Containers.CreateContainerAsync(new CreateContainerParameters
                {
                    Image = "mcr.microsoft.com/mssql/server:2022-latest",
                    Name = "Frituurtje",
                    Env = new List<string>
                    {
                        "ACCEPT_EULA=Y",
                        "SA_PASSWORD=Ki3k3frut-03!"
                    },
                    HostConfig = new HostConfig
                    {
                        PortBindings = new Dictionary<string, IList<PortBinding>>
                        {
                            { "1433/tcp", new List<PortBinding> { new PortBinding { HostPort = "13413" } } }
                        }
                    }
                });
                await Task.Delay(TimeSpan.FromSeconds(20));
                await client.Containers.StartContainerAsync("frituurtje", new ContainerStartParameters());
            }
            else if(container.State != "running")
            {
                // Start the container if it exists but is not running
                await client.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());
            }
        }
    }
}
