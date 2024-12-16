namespace Syntra.Frituurtje.Helpers
{
    public class Calculations
    {
        public decimal ToCelcius(decimal? farenheit) => Math.Round((farenheit - 32m) * 5m / 9m ?? 0, 2);
        public decimal ToFarenheit(decimal? celcius) => Math.Round(celcius * 9m / 5m + 32m ?? 0, 2);
        public decimal? CalculateToCelcius(decimal? farenheit, decimal? celcius) => ToFarenheit(farenheit) + celcius;
    }
}
