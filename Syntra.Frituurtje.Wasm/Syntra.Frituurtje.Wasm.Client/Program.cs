using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syntra.Frituurtje.Wasm.Client.Services;
using Syntra.Frituurtje.Wasm.Shared;

namespace Syntra.Frituurtje.Wasm.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddScoped(s => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
       //     builder.Services.AddHttpClient<IMenuService, MenuClientService>(c => new HttpClient() { BaseAddress= new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IMenuService,MenuClientService>();
            await builder.Build().RunAsync();
        }
    }
}
