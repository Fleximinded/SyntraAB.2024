using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.Model
{
    public class ZipCode
    {
        public int Zipcode { get; set; }
        public string City { get; set; } = "";
        public override string ToString() => $"{Zipcode}\t{City}";
    }
}
