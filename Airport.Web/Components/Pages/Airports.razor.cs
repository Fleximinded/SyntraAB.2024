using Airport.Data.Models;
using Airport.Web.Services.Define;
using Airport.Web.Services.Src;
using Microsoft.AspNetCore.Components;

namespace Airport.Web.Components.Pages
{
    public partial class Airports
    {
        [Inject]
        public IAirportService Service { get; set; } = default!;
        public List<AirportInfo>? AirportsList { get; set; } = null;

        string searchName = string.Empty;   
        string searchIata = string.Empty;
        string searchCountry = string.Empty;
        void Search()
        {
            AirportsList = Service.Find(searchName, searchIata, searchCountry);
            StateHasChanged();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                AirportsList = Service.GetAllAirports(0, 100);
                StateHasChanged();
            }
        }
    }
}
