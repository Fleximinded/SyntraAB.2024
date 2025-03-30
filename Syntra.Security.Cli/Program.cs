using Fleximinded.Core.Parts.CLI;
using Syntra.Security.Encrypt;

namespace Syntra.Security.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CliRuntime cli = new();
            cli.AddExecutor(new HashFunctions());
            cli.AddExecutor(new SymmetricEncryption());
            cli.AddExecutor(new AsymmetricEncryption());
            cli.Execute();
        }
    }
}
