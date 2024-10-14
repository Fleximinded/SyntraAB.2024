using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Source
{
    public class ThreadingDemo
    {
        static long i = 0;
        public static void BlockMyApp() {
            while(true)
            {
                i++;
            }
            Console.WriteLine(i.ToString());
        }
        public static void LetMeWaitAWhile()
        {
            Thread t = new Thread(CalculateStupidThings);
            t.Start();
            Console.WriteLine(i.ToString());
        }
        public static void CalculateStupidThings() {
            while(i < 50000000000)
            {
                i++;
            }

        }
        public static void Demo1() {
            Thread t = new Thread(WriteXToConsole1000x);
            t.Start();
            WriteOToConsole1000x();
            Console.WriteLine();
        }
        public static void Demo2()
        {
            Thread t = new Thread(WriteXToConsole1000x);
            t.Start();
            t.Join();
            WriteOToConsole1000x();
            Console.WriteLine();
        }
        public static void Demo3()
        {
            Thread t1 = new Thread(WriteXToConsole1000x);
            Thread t2= new Thread(WriteOToConsole1000x);  
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();            
            Console.WriteLine("Gedaan!");
            Console.WriteLine();

        }
        public static void Demo4(string x,string y)
        {
            Thread t1 = new Thread(WriteCharToConsole1000x);
            Thread t2 = new Thread(WriteCharToConsole1000x);
            t1.Start(x);
            t2.Start(y);
            t1.Join();
            t2.Join();
            Console.WriteLine("Gedaan!");
            Console.WriteLine();

        }
        public static void Demo5(string x, string y)
        {
            Thread t1 = new Thread(() => { for(int i = 0; i < 1000; i++) { Console.Write(x); } });
            Thread t2 = new Thread(() => { for(int i = 0; i < 1000; i++) { Console.Write(y); } });
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine("Gedaan!");
            Console.WriteLine();

        }
        public static void WriteXToConsole1000x()
        {
            for(int i = 0; i < 1000; i++)
            {
                Console.Write($"X");
            }
        }
        public static void WriteOToConsole1000x()
        {
            for(int i = 0; i < 1000; i++)
            {
                Console.Write($"O");
            }
        }
        public static void WriteCharToConsole1000x(object? src)
        {
            string ch = src as string ?? " ";  
            for(int i = 0; i < 1000; i++)
            {
                Console.Write(ch);
            }
        }
        public static void ExceptionDemo()
        {
            try
            {
                Thread t = new Thread(ThrowException);
                t.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }   
            Console.WriteLine("Gedaan!");
        }
        public static void ThrowException() {
            string? var1 = null;
            Thread.Sleep(1000);
            Console.WriteLine(var1.ToUpper());
        }
        public static void ExceptionDemoCatched()
        {
            try
            {
                Thread t = new Thread(CatchException);
                t.Start();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Gedaan!");
        }
        public static void CatchException()
        {
            try
            {
                string? var1 = null;
                Thread.Sleep(1000);
                Console.WriteLine(var1.ToUpper());
            } catch(Exception ex)
            {
                Console.WriteLine($"Exeption catched in tread: {ex.Message}");
            }   
        }
        public static void LockExample() { 
        
        
        }
    }
}
