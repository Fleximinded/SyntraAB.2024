using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleximinded.Core.Extensions;

namespace DLinkedList.Test
{
    public class MyDog
    {
        public MyDog()        {                        }
        public MyDog(string key,string name,DateOnly birthdate)
        {
            Key = key;
            Name = name;
            BirthDate = birthdate;
        }
        public string Key { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
        public int Age { get => BirthDate.TimeSpanInYears(); }
        public override string ToString()
        {
            return $"This dog is called {Name} with chip ID '{Key}' and is born on {BirthDate.ToShortDateString()} => ({Age} years old). ";
        }
    }
}
