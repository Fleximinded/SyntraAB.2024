using Airport.Data.Models;

namespace Airport.Web.Services.Define
{
    public interface IAirportService
    {
        List<AirportInfo> GetAllAirports(int start = 0, int take = -1);
        AirportInfo? GetAirport(string id);
        List<AirportInfo> Find(string? name,string? iata=null,string? country=null);
        List<AirportInfo> Find(AirportInfo airport, double range);
    }
}
