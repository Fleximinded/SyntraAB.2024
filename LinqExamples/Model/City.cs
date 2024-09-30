using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinqExamples.Model
{
    public static class ImportExtensions {
        public static City[] ImportZipCodes(this string path) {
            if(Path.Exists(path))
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                string source=File.ReadAllText(path);
                var res=JsonSerializer.Deserialize<City[]>(source,options);
                return res??[];
            }
            return [];
        }
        public static string ExportToJson<T>(this T obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(obj, options);
        }
    
    }
    public class City
    {
        public string country_code { get; set; } = string.Empty;
        public string zipcode { get; set; } = string.Empty;
        public string place { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string state_code { get; set; } = string.Empty;
        public string province { get; set; } = string.Empty;
        public string province_code { get; set; } = string.Empty;
        public string community { get; set; } = string.Empty;
        public string community_code { get; set; } = string.Empty;
        public string latitude { get; set; } = string.Empty;
        public string longitude { get; set; } = string.Empty;
        public override string ToString() => $"{zipcode} - {place} ({country_code} => latitude: {latitude} longitude: {longitude})";
    }
}
