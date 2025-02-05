using Syntra.Fietshersteller.Db.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Fietshersteller.Db.Models
{
    public class Person : DbBase
    {
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(150)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Street { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Number { get; set; } = string.Empty;
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;
        [MaxLength(150)]
        public string City { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Country { get; set; } = string.Empty;
        [MaxLength(150)]
        public string? ExternalId { get; set; }
        [MaxLength(3500)]
        public string? Data { get; set; }
    }
}
