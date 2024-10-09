


using Fleximinded.Core.Parts.CLI;
using DLinkedList;
using LambdaTest;
using LinqExamples;
using ConcurrencyDemo;

namespace SyntraAB._2024.cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CliRuntime runtime = new CliRuntime();
            runtime.AddExecutor(new DlinkListCli());
            runtime.AddExecutor(new LambdaTestLib());
            runtime.AddExecutor(new ConcurrencyCli());
            runtime.AddExecutor(new LinqCli());
            runtime.Execute();
        }
    }
}
