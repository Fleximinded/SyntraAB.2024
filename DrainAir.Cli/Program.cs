using Fleximinded.Core.Parts.CLI;

namespace DrainAir.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CliRuntime rt = new CliRuntime();
            //rt.AddExecutor(new EfCommands());
            rt.Execute();
        }
    }
}
