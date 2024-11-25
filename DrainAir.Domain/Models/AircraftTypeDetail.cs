using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("AircraftTypeDetail")]
public partial class AircraftTypeDetail
{
    [Key]
    [Column("AircraftTypeID")]
    public byte AircraftTypeId { get; set; }

    public byte? TurbineCount { get; set; }

    public float? Length { get; set; }

    public short? Tare { get; set; }

    public string? Memo { get; set; }

    [ForeignKey("AircraftTypeId")]
    [InverseProperty("AircraftTypeDetail")]
    public virtual AircraftType AircraftType { get; set; } = null!;
}
