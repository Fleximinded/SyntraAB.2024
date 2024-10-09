using System.Text.Json.Serialization;

namespace Airport.Data.Models
{
   
    public class AirportInfo
    {
        [JsonPropertyName("objectID")]
        public string Id { get; set; }=string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;    
        public string Country { get; set; } = string.Empty; 
        [JsonPropertyName("Iata_code")]
        public string IataCode { get; set; } = string.Empty;
        public GeoLocation Location { get; set; } = new GeoLocation();
        [JsonPropertyName("links_count")]
        public int LinksTo{ get; set; }
        public override string ToString() => $"{IataCode} - {Name} ({City} => {Country})";
    }

}
