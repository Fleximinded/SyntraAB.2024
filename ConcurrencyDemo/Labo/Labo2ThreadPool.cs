using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Labo
{
    public class ThreadPoolLabo
    {
        public static void Labo1()
        {
            ThreadPool.QueueUserWorkItem(TaskMethod, "Task 1");
            ThreadPool.QueueUserWorkItem(TaskMethod, "Task 2");
            ThreadPool.QueueUserWorkItem(TaskMethod, "Task 3");

            Thread.Sleep(3000);
        }

        static void TaskMethod(object? state)
        {
            string taskName = state as string ?? "No state given";
            Console.WriteLine($"{taskName} is executed on thread: {Thread.CurrentThread.ManagedThreadId}");
        }
        public static void Labo2()
        {
            ThreadPool.QueueUserWorkItem(LaboShared.CalculateCube, 3);
            ThreadPool.QueueUserWorkItem(LaboShared.CalculateCube, 4);
            ThreadPool.QueueUserWorkItem(LaboShared.CalculateCube, 5);

            Thread.Sleep(2000);
        }


    }
}
