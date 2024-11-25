using Fleximinded.Core.Parts.CLI;
using Syntra.EF.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Cli
{
    public class EfCommands : ICliExecutable
    {
        TimeRegistrationContext _context = new TimeRegistrationContext();
        public string Name => "TimeRegistration";
        public string Description => Name;

        public bool Execute(ICliRuntime owner, ICliCommand prm)
        {
            switch(prm.Command)
            {
                case "show":
                    if(prm.ContainsValue("employees"))
                    {
                        var result=_context.Employees.ToList();
                        foreach(var emp in result)
                        {
                            Console.WriteLine($"{emp.Id} {emp.FirstName} {emp.LastName} {emp.EmployeeNumber}");
                        }
                    }
                    if(prm.ContainsValue("persons"))
                    {
                        var result = _context.Person.ToList();
                        foreach(var emp in result)
                        {
                            Console.WriteLine($"{emp.Id} {emp.FirstName} {emp.LastName} {emp.BirthDate.ToLongDateString()}");
                        }
                    }
                    return true;
            }
            return false;
        }
    }
}
