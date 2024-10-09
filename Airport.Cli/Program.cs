using Airport.Data.Repository;



namespace Airport.Cli
{
    internal class Program
    {
        static Airports? AirportRepo { get; set; }
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, World!");
            AirportRepo = Airports.FromResource();
        }
    }
}
