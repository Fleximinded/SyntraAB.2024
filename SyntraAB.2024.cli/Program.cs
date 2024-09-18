


using Fleximinded.Core.Parts.CLI;
using DLinkedList;

namespace SyntraAB._2024.cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CliRuntime runtime = new CliRuntime();
            runtime.AddExecutor(new DlinkListCli());
            runtime.Execute();
        }
    }
}
