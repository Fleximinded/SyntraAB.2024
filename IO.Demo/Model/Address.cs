using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IO.Demo.Model
{
    public class StreetData {
        public string Value { get; set; } = default!;
        [XmlAttribute("Nr")]
        public string HouseNumber { get; set; } = default!;
    }
    public class Address
    {
        public Address() { }
        public Address(string street,string HousNumber, string city, string postalCode, string country)
        {
            Street.Value = street;
            City = city;
            Street.HouseNumber = HousNumber;
            PostalCode = postalCode;
            Country = country;
        }
        public StreetData Street { get; set; } = new StreetData();
        public string City { get; set; } = default!;
        [XmlIgnore]
        public string? State { get; set; }  ="City state of Merksem";
        [XmlElement("ZipCode")]
        public string PostalCode { get; set; } = default!;
        public string Country { get; set; }=default!;

    }
}
