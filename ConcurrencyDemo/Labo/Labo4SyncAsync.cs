using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Labo
{

    public class SyncAsyncLabo
    {
       public static async Task Labo1()
        {
            Console.WriteLine("Loading data...");
            await FetchDataAsync();
            Console.WriteLine("Data complete");
        }

        static async Task FetchDataAsync()
        {
            await Task.Delay(3000);
            Console.WriteLine("Data is loaded");
        }
        public static async Task Labo2()
        {
            try
            {
                await TaskWithException();
            } catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static async Task TaskWithException()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException("There was a non expected unhandled error while performing this marvelous task, so it failed completely. I'm sorry, better next time");
        }
        public static async Task Labo3()
        {
            Task task1 = FetchDataAsync(2);
            Task task2 = FetchDataAsync(4);
            Task task3 = FetchDataAsync(6);

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine("Data loaded!");
        }

        static async Task FetchDataAsync(int seconds)
        {
            Console.WriteLine($"Loading date, there is a {seconds} seconds delay...");
            await Task.Delay(seconds * 1000);
            Console.WriteLine($"Data loaded after {seconds} seconds.");
        }
        static async Task Labo4()
        {
            Task longRunningTask = SimulateLongRunningTask();
            Task timeoutTask = Task.Delay(5000);

            if(await Task.WhenAny(longRunningTask, timeoutTask) == timeoutTask)
            {
                Console.WriteLine("Expired!");
            }
            else
            {
                Console.WriteLine("Task completed with no errors.");
            }
        }

        static async Task SimulateLongRunningTask()
        {
            Console.WriteLine("Task in progress...");
            await Task.Delay(10000);
            Console.WriteLine("Done!");
        }
    }
}

