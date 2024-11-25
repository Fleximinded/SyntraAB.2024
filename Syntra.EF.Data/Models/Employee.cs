using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Data.Models
{
    [Table("Employee")]
    public class Employee :Person
    {
        public Employee() { }
        public Employee(string name, string firstname, DateTime birthDate) : base(name, firstname, birthDate) { }
        public string EmployeeNumber { get; set; } = "";
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<TimeRegistration> TimeRegistrations { get; set; } = [];
    }
}
