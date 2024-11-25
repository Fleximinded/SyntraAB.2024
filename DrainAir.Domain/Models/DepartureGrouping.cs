using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("DepartureGrouping")]
public partial class DepartureGrouping
{
    [Key]
    public string Departure { get; set; } = null!;

    public int FlightCount { get; set; }
}
