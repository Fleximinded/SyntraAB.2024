using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("Passenger")]
[Index("DetailId", Name = "IX_Passenger_DetailID")]
public partial class Passenger
{
    public DateTime? Birthday { get; set; }

    [Column("DetailID")]
    public int? DetailId { get; set; }

    [Key]
    [Column("PersonID")]
    public int PersonId { get; set; }

    public string? Surname { get; set; }

    public string? GivenName { get; set; }

    [Column("EMail")]
    public string? Email { get; set; }

    public DateTime? CustomerSince { get; set; }

    public bool? FrequentFlyer { get; set; }

    [StringLength(1)]
    public string Status { get; set; } = null!;

    [ForeignKey("DetailId")]
    [InverseProperty("Passengers")]
    public virtual Persondetail? Detail { get; set; }

    [ForeignKey("PassengerId")]
    [InverseProperty("Passengers")]
    public virtual ICollection<Flight> FlightNos { get; set; } = new List<Flight>();
}
