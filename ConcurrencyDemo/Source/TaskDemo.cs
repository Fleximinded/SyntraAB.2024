using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Source
{
    public class TaskDemo
    {
        public void ResultDemo()
        {
            try
            {
                Task<string> myTask = Task.Run(() => CountNumbers());
                Console.WriteLine(myTask.Result);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error in our task: {ex.Message}");
            }
        }
        public void ExceptionDemo()
        {
            try
            {
                Task<string> myTask = Task.Run(() => CountNumbers(true));
                Console.WriteLine(myTask.Result);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error in our task: {ex.Message}");
            }
        }
        public string CountNumbers(bool doCrash = false, bool doItSloooooooooooow = false)
        {
            int numIterations = doItSloooooooooooow ? 2000 : 10000;            
            string result = "";
            string? sep = null;
            int y = 0;
            if(doCrash) Console.WriteLine(sep.ToLower());
            for(int i = 0; i < numIterations; i++)
            {
                if(doItSloooooooooooow) Task.Delay(1).Wait();
                result += $"{sep ?? ""}{i + y}";
                sep ??= ",";
                y = i;
            }
            return result;
        }
        public void ContinueDemo()
        {
            Task<string> myTask = Task.Run(() => CountNumbers(doItSloooooooooooow: true));
            myTask.ContinueWith((prevTask) =>
            {
                Console.WriteLine($"The result of the previous task is: {prevTask.Result}");
            });
        }
        public void ContinueTaskDemo()
        {
             Task.Run(() => CountNumbers(doItSloooooooooooow: true))
                .ContinueWith(result => Console.WriteLine($"The result of the previous task is: {result.Result}"));
        }
    }
}
