using System.Reflection.Emit;
using ConcurrencyDemo.Labo;
using ConcurrencyDemo.Source;
using Fleximinded.Core.Parts.CLI;

namespace ConcurrencyDemo
{
    public class ConcurrencyCli : ICliExecutable
    {
        bool LaboMode { get; set; } = false;
        public string Name { get => "Concurrency"; }
        public string Description { get => "This is a Async test"; }
        public bool Execute(ICliRuntime owner, ICliCommand prm)
        {
            if(LaboMode) return ExecuteLabo(owner,prm);
            switch(prm.Command)
            {
                case "mode":
                    LaboMode = prm.ContainsOption("labo");
                    return true;
                case "threads.block":
                    ThreadingDemo.BlockMyApp();
                    return true;
                case "threads.lotofwork":
                    ThreadingDemo.LetMeWaitAWhile();
                    return true;
                case "threads.demo1":
                    ThreadingDemo.Demo1();
                    return true;
                case "threads.demo2":
                    ThreadingDemo.Demo2();
                    return true;
                case "threads.demo3":
                    ThreadingDemo.Demo3();
                    return true;
                case "threads.demo4":
                    string x = prm.FindOption("x")?.Value ?? "X";
                    string y = prm.FindOption("y")?.Value ?? "O";
                    ThreadingDemo.Demo4(x, y);
                    return true;
                case "threads.demo5":
                    string x2 = prm.FindOption("x")?.Value ?? "X";
                    string y2 = prm.FindOption("y")?.Value ?? "O";
                    ThreadingDemo.Demo5(x2, y2);
                    return true;
                case "threads.crash":
                    ThreadingDemo.ExceptionDemo();
                    return true;
                case "treads.catch":
                    ThreadingDemo.ExceptionDemoCatched();
                    return true;
                case "lock.demo":
                    LockDemoClass demo = new LockDemoClass();
                    demo.Execute();
                    return true;
                case "threadpool.info":
                    ThreadPoolInfo info = new ThreadPoolInfo();
                    info.GetThreadPoolInfo();
                    return true;
                case "task.result":
                    TaskDemo taskDemo = new TaskDemo();
                    taskDemo.ResultDemo();
                    return true;
                case "task.exception":
                    TaskDemo exeptionDemo = new TaskDemo();
                    exeptionDemo.ExceptionDemo();
                    return true;
                case "task.continue":
                    TaskDemo continueDemo = new TaskDemo();
                    if(prm.ContainsOption("-2"))
                        continueDemo.ContinueTaskDemo();
                    else
                        continueDemo.ContinueDemo();
                    return true;

            }
            return false;
        }
        public bool ExecuteLabo(ICliRuntime owner, ICliCommand prm)
        {


            switch(prm.Command)
            {
                case "mode":
                    LaboMode = prm.ContainsOption("labo");
                    return true;
                case "thread.1":
                    ThreadLabo.Labo1();
                    return true;
                    case "thread.2":
                    ThreadLabo.Labo2();
                    return true;
                    case "thread.3":
                    ThreadLabo.Labo3();
                    return true;
                    case "task.1":
                    TaskLabo.Labo1();
                    return true;
                    case "task.2":
                    Task.Run(() => TaskLabo.Labo2()).Wait();
                    return true;
                    case "syncasync.1":
                    Task.Run(() => SyncAsyncLabo.Labo1()).Wait();
                    return true;
                   


            }
            return false;
        }
    }
}
