using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("Airline")]
public partial class Airline
{
    [Key]
    [StringLength(3)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Address { get; set; }

    public int? FoundingYear { get; set; }

    public bool? Bunkrupt { get; set; }

    [InverseProperty("AirlineCodeNavigation")]
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
