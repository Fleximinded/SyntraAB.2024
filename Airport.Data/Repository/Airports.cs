using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Airport.Data.Models;

namespace Airport.Data.Repository
{
    public class Airports
    {
        public Airports() { }
        public List<AirportInfo> List { get; set; } = [];
        public bool Import(string jsonFile)
        {//...
            return false;
        }
    }
}
