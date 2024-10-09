using Airport.Data.Models;
using Airport.Data.Repository;
using Airport.Web.Services.Define;

namespace Airport.Web.Services.Src
{
    public class AirportService : IAirportService
    {
        Airports Repository { get; init; }
        public AirportService()
        {
            Repository = Airports.FromResource() ?? new Airports();
        }
        public List<AirportInfo> Find(string? name, string? iata = null, string? country = null)
        {
            IEnumerable<AirportInfo> result = Repository.List;
            if(!string.IsNullOrWhiteSpace(name))
            {
                result=result.Where(q=>q.Name.Contains(name,StringComparison.CurrentCultureIgnoreCase)); 
            }
            if(!string.IsNullOrWhiteSpace(country)) { 
                result=result.Where(q=>q.Country.Contains(country,StringComparison.CurrentCultureIgnoreCase)); 
            }
            if(!string.IsNullOrWhiteSpace(iata)) { 
                result=result.Where(q=>q.IataCode.Contains(iata,StringComparison.CurrentCultureIgnoreCase)); 
            }
            return result.ToList();
        }

        public List<AirportInfo> Find(AirportInfo airport, double range)
        {
            return [];
        }

        public AirportInfo? GetAirport(string id)=>
            Repository.List.Where(t => t.Id == id).FirstOrDefault();

        public List<AirportInfo> GetAllAirports(int start = 0, int take = -1)
        {
            IEnumerable<AirportInfo> result = Repository.List;
            if(start > 0) { 
                result=result.Skip(start); 
            }
            if(take > 0)
            {
                result=result.Take(take);
            }
            return result.ToList();
        }
    }
}
