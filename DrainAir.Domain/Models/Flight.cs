using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("Flight")]
[Index("AircraftTypeId", Name = "IX_Flight_AircraftTypeID")]
[Index("AirlineCode", Name = "IX_Flight_AirlineCode")]
[Index("CopilotId", Name = "IX_Flight_CopilotId")]
[Index("Departure", "Destination", Name = "IX_Flight_Departure_Destination")]
[Index("PilotId", Name = "IX_Flight_PilotId")]
[Index("FreeSeats", Name = "Index_FreeSeats")]
public partial class Flight
{
    [Key]
    public int FlightNo { get; set; }

    [StringLength(50)]
    public string? Departure { get; set; }

    [StringLength(50)]
    public string? Destination { get; set; }

    public DateTime FlightDate { get; set; }

    public bool? NonSmokingFlight { get; set; }

    public short Seats { get; set; }

    public short? FreeSeats { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public string? Memo { get; set; }

    public bool? Strikebound { get; set; }

    [Column(TypeName = "numeric(20, 8)")]
    public decimal? Utilization { get; set; }

    public byte[]? Timestamp { get; set; }

    [StringLength(3)]
    public string? AirlineCode { get; set; }

    public int PilotId { get; set; }

    public int? CopilotId { get; set; }

    [Column("AircraftTypeID")]
    public byte? AircraftTypeId { get; set; }

    public DateTime LastChange { get; set; }

    [ForeignKey("AircraftTypeId")]
    [InverseProperty("Flights")]
    public virtual AircraftType? AircraftType { get; set; }

    [ForeignKey("AirlineCode")]
    [InverseProperty("Flights")]
    public virtual Airline? AirlineCodeNavigation { get; set; }

    [ForeignKey("CopilotId")]
    [InverseProperty("FlightCopilots")]
    public virtual Employee? Copilot { get; set; }

    [ForeignKey("PilotId")]
    [InverseProperty("FlightPilots")]
    public virtual Employee Pilot { get; set; } = null!;

    [ForeignKey("FlightNo")]
    [InverseProperty("FlightNos")]
    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
