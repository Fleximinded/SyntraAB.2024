using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConcurrencyDemo.Source
{
    public class ThreadPoolInfo
    {
        public void GetThreadPoolInfo() {
            
            ThreadPool.GetMaxThreads(out int maxThreads, out int completionThreads);
            ThreadPool.GetMinThreads(out int minThreads,out int minCompletionThreads);
            Console.WriteLine($"Number of cores available: {Environment.ProcessorCount}");
            Console.WriteLine($"Max number of Work threads {maxThreads}");
            Console.WriteLine($"Max number of Completion threads {completionThreads}");
            Console.WriteLine($"Min number of Work threads {minThreads}");
            Console.WriteLine($"Min number of Completion threads {minCompletionThreads}");
            Console.WriteLine($"Current number of existing threads {ThreadPool.ThreadCount}");           
        
        }
    }
}
