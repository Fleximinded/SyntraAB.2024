using Fleximinded.Core.Parts.CLI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Syntra.EF.Domain;

namespace Syntra.EF.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CliRuntime rt = new CliRuntime();
            rt.AddExecutor(new EfCommands());
            rt.Execute();
        }
    }
}
