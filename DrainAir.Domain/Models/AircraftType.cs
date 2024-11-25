using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("AircraftType")]
public partial class AircraftType
{
    [Key]
    [Column("TypeID")]
    public byte TypeId { get; set; }

    public string? Manufacturer { get; set; }

    public string? Name { get; set; }

    [InverseProperty("AircraftType")]
    public virtual AircraftTypeDetail? AircraftTypeDetail { get; set; }

    [InverseProperty("AircraftType")]
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
