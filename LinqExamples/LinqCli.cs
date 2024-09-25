using Fleximinded.Core.Parts.CLI;
using LinqExamples.Model;
using LinqExamples.Source;
using System.Diagnostics.CodeAnalysis;

namespace LinqExamples
{
    public class LinqCli : ICliExecutable
    {
        public string Name { get => "LinqExample"; }
        public string Description { get => "This is a LINQ test"; }
        public bool Execute(ICliRuntime owner, ICliCommand prm)
        {
            List<City> res = @"d:\data\syntra\BE\zip.json".ImportZipCodes()?.ToList() ?? [];
            IEnumerable<City> cities = default!;
            var findZip = prm.FindOption("zip")?.Value;
            var findPlace = prm.FindOption("place")?.Value;
            if(res?.Count > 0)
            {
                switch(prm.Command)
                {
                    case "linqquery":
                        cities = res;
                        if(findZip != null)
                        {
                            cities = cities.Where(t => t.zipcode.Contains(findZip));
                        }
                        if(findPlace != null)
                        {
                            cities ??= res;
                            cities = cities.Where(t => t.place?.Contains(findPlace)==true);
                        }
                        cities=cities.OrderBy(t => t.zipcode).ThenBy(t => t.place);
                        res.RemoveAt(0);
                        foreach(City city in cities) { Console.WriteLine(city); }
                        return true;
                    case "ownquery":
                        cities = res;
                        if(findZip != null)
                        {
                            cities = cities.OwnWhere(t => t.zipcode?.Contains(findZip)==true);
                        }
                        if(findPlace != null)
                        {
                            cities = cities.OwnWhere(t => t.place?.Contains(findPlace)==true);
                        }
                        cities=cities.OrderBy(t => t.zipcode).ThenBy(t => t.place);                        
                        res.RemoveAt(0);
                        foreach(City city in cities) { Console.WriteLine(city); }
                        return true;
                    case "tupperquery":
                        cities = res;
                        if(findZip != null)
                        {
                            cities = cities.TupperWhere(t => t.zipcode?.Contains(findZip)==true);
                        }
                        if(findPlace != null)
                        {
                            cities = cities.TupperWhere(t => t.place?.Contains(findPlace)==true);
                        }
                        cities=cities.OrderBy(t => t.zipcode).ThenBy(t => t.place);
                        res.RemoveAt(0);
                        foreach(City city in cities) { Console.WriteLine(city); }
                        return true;
                    case "kiss":
                        cities = res;
                        if(findZip != null)
                        {
                            cities = cities.Where(t => t.zipcode.Contains(findZip));
                        }
                        if(findPlace != null)
                        {
                            cities ??= res;
                            cities = cities.Where(t => t.place?.Contains(findPlace) == true);
                        }
                        var result = cities.Select(s=> new ZipCode() { Zipcode=int.Parse(s.zipcode),City=s.place}).OrderBy(t => t.Zipcode).ThenBy(t => t.City);
                        foreach(ZipCode city in result) { Console.WriteLine(city); }
                        return true;
                }
            }
            return false;
        }
        //bool PredicateAsFunction(City src) {
        //    return src.zipcode.Contains(zipcode);
        //}
        private void TestSimpleLinq()
        {
        }
    }
}
