


using Fleximinded.Core.Parts.CLI;
using DLinkedList;
using LambdaTest;
using LinqExamples;
using ConcurrencyDemo;
using IO.Demo;

namespace SyntraAB._2024.cli
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            CliRuntime runtime = new CliRuntime();
            runtime.AddExecutor(new DlinkListCli());
            runtime.AddExecutor(new LambdaTestLib());
            runtime.AddExecutor(new ConcurrencyCli());
            runtime.AddExecutor(new LinqCli());
            runtime.AddExecutor(new IOLib());
            runtime.Execute();
        }
    }
}
