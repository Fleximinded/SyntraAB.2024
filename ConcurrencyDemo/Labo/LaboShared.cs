using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Labo
{
    public static class LaboShared
    {
        public static void PrintMessage(string message,int sleep=-1)
        {
            if(sleep > 0) { Thread.Sleep(5000); }
            Console.WriteLine(message);
        }
        public static void CalculateSquare(int number)=> PrintMessage($"The square number of {number} is {SquareNumber(number)}");       
       public static int DoubleNumber(int number)=> number * 2;
        public static int SquareNumber(int number) => number * number;
        public static void CalculateCube(object? state)
        {
            int number = state as int? ??-1;
            Console.WriteLine($"The Cube of {number} is {number * number * number}");
        }
    }
}
