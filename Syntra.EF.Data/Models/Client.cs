using Syntra.EF.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Data.Models
{
    public class Client :DbBase
    {
        public Client() { }
        [StringLength(300)]
        public string CompanyName { get; set; } = "";
        [StringLength(100)]
        public string? Vat { get; set; }
        public Person? Contact { get; set; }
    }
}
