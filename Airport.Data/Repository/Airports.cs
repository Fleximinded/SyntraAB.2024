using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Airport.Data.Models;

namespace Airport.Data.Repository
{
    public class Airports
    {
        public Airports() { }
        public List<AirportInfo> List { get; set; } = [];
        public static Airports? FromJson(string jsonFile)
        {
            try
            {
                if(File.Exists(jsonFile))
                {
                    JsonSerializer.Deserialize<Airports>(jsonFile);
                }
            } catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }
        public bool Import(string jsonfile)
        {
            var obj = FromJson(jsonfile);
            if(obj != null)
            {
                List = obj.List;
                return true;
            }
            return false;
        }
        public static Airports? FromResource()
        {
            var json = FindResource("airports.json");
            if(json != null)
            {
                return JsonSerializer.Deserialize<Airports>(json);
                
            }
            return null;
        }
        public static string? FindResource(string name)
        {
            var assm = typeof(Airports).Assembly;
            string? resName = assm.GetManifestResourceNames().Where(t => t.Contains(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if(resName != null)
            {
                using Stream? strm = assm.GetManifestResourceStream(resName);
                if(strm != null)
                {
                    using StreamReader streamReader = new StreamReader(strm);
                    return streamReader.ReadToEnd();
                }
            }
            return null;
        }
    }
}
