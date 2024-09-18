using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLinkedList.Test
{
    public class TestMethods
    {
        static MyDog[] MyDogs = [
            new MyDog("Hogwarts-001-HGD","Fluffie",new DateOnly(1765,4,1)),
            new MyDog("TVSer-svts-007","Lassie",new DateOnly(1975,7,20)),
            new MyDog("Haddk-tintin-herge","Bobbie",new DateOnly(1951,1,1)),
            new MyDog("Bkljou-bompie-onooozelmanneke-Gamma1","Blakie",new DateOnly(2005,11,1))
        ];

        static DLinkedList<string, MyDog> DogList = new DLinkedList<string, MyDog>();
        static public string FindText { get; set; } = "";
        public static void Init()
        {
            DogList.Clear();
            foreach (var dog in MyDogs)
            {
                DogList.Add(dog.Key, dog);
            }
            Console.WriteLine("List of dogs");
            foreach(var dog in DogList)
            {
                Console.WriteLine(dog);
            }
        }   
        public static void Test1()
        {
            Init();           
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("Remove Lassie");
            DogList.Remove("TVSer-svts-007");
            foreach (var dog in DogList)
            {
                Console.WriteLine(dog);
            }
        }
        // Func<in TValue, out bool> predicate
        public static bool FindAll(MyDog dog)
        {
            return dog.Name?.Contains(FindText, StringComparison.InvariantCultureIgnoreCase) == true;
        }
        public static bool FindExact(MyDog dog)
        {
            return dog.Name == FindText;
        }
        public static void TestFind(bool exact = false)
        {
            Init();
            if(string.IsNullOrWhiteSpace(FindText))
            {
                Console.WriteLine("Find ");
                FindText = Console.ReadLine() ?? "";
            }           
             var result = DogList.Find(exact?FindExact: FindAll);           
            Console.WriteLine();

            Console.WriteLine("Result of your search:");
            if(result?.Count > 0)
            {
                foreach(var dog in result)
                {
                    Console.WriteLine(dog);
                }
            }
            else
            {
                Console.WriteLine("No dogs found");
            }            
        }

    }
}
