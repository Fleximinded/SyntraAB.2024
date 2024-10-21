using Fleximinded.Core.Extensions;
using IO.Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IO.Demo.src
{
    public class XmlDemo
    {
        public static void Demo1(string? saveTo=null)
        {
            Address address = new Address("Tramezantlei","69","Aaaantwaarpe","2000","Kingdom of Antwerpen");
            StringWriter writer = new StringWriter();   
            XmlSerializer serializer=new XmlSerializer(typeof(Address));
            serializer.Serialize(writer,address);
            Console.WriteLine(writer.ToString());
            if(saveTo.NotEmpty() && Directory.Exists(Path.GetDirectoryName(saveTo))){
                File.WriteAllText(saveTo, writer.ToString());
                Console.WriteLine($"Xml written to {saveTo}");
            }

        }
        public static void Demo2(string loadFrom)
        {
            string xml = File.ReadAllText(loadFrom);    
            StringReader xmlReader = new StringReader(xml); 
            XmlSerializer serializer = new XmlSerializer(typeof(Address));
            Address? theAddress=serializer.Deserialize(xmlReader) as Address;
            if(theAddress!=null)
            {
                Console.WriteLine($"Address: {theAddress.Street.Value} {theAddress.Street.HouseNumber}");
                Console.WriteLine($"City: {theAddress.City}");
                Console.WriteLine($"PostalCode: {theAddress.PostalCode}");
                Console.WriteLine($"Country: {theAddress.Country}");
            }
            

        }
    }
}
