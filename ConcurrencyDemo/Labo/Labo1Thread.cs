using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Labo
{
    public class ThreadLabo
    {

        private static int sharedCounter = 0;
        private static readonly object lockObject = new object();
        public static void Labo1()
        {
            Thread t1 = new Thread(() => LaboShared.PrintMessage($"Thread 1 works", 5000));
            Thread t2 = new Thread(() => LaboShared.PrintMessage($"Thread 2 works", 5000));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

       
        public static void Labo2()
        {
            Thread t1 = new Thread(() => LaboShared.CalculateSquare(5));
            Thread t2 = new Thread(() => LaboShared.CalculateSquare(10));
            Thread t3 = new Thread(() => LaboShared.CalculateSquare(15));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

        public static void Labo3()
        {
            Thread t1 = new Thread(IncrementCounter);
            Thread t2 = new Thread(IncrementCounter);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"The actual result is: {sharedCounter}");
        }

        static void IncrementCounter()
        {
            for(int i = 0; i < 1000; i++)
            {
                lock(lockObject)
                {
                    sharedCounter++;
                }
            }
        }
    }
}
