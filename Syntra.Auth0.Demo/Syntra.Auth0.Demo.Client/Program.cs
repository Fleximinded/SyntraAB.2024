using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace Syntra.Auth0.Demo.Client;
using Microsoft.AspNetCore.Components.Authorization;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();
        
        await builder.Build().RunAsync();
    }
}
