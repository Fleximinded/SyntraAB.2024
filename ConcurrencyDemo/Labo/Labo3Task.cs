using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Labo
{
    public class TaskLabo
    {
        public static async Task Labo1()
        {
            Task<int> t1 = Task.Run(() => LaboShared.DoubleNumber(5));
            Task<int> t2 = Task.Run(() => LaboShared.DoubleNumber(10));
            Task<int> t3 = Task.Run(() => LaboShared.DoubleNumber(15));

            int[] results = await Task.WhenAll(t1, t2, t3);
            foreach(var result in results)
            {
                Console.WriteLine($"Result: {result}");
            }
        }

       
        public static async Task Labo2()
        {
            Task<int> t1 = Task.Run(() => LaboShared.DoubleNumber(5));
            Task<int> t2 = t1.ContinueWith(previousTask => LaboShared.DoubleNumber(previousTask.Result));
            Task<int> t3 = t2.ContinueWith(previousTask => LaboShared.SquareNumber(previousTask.Result));
            int result = await t3;
            Console.WriteLine($"The result is: {result}");
        }


      
    }
}
