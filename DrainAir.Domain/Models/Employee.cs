using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("Employee")]
[Index("DetailId", Name = "IX_Employee_DetailID")]
[Index("SupervisorPersonId", Name = "IX_Employee_SupervisorPersonID")]
public partial class Employee
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

    public float Salary { get; set; }

    [Column("SupervisorPersonID")]
    public int? SupervisorPersonId { get; set; }

    public string? PassportNumber { get; set; }

    public string Discriminator { get; set; } = null!;

    public DateTime? LicenseDate { get; set; }

    public int? FlightHours { get; set; }

    public string? PilotLicenseType { get; set; }

    [StringLength(50)]
    public string? FlightSchool { get; set; }

    [ForeignKey("DetailId")]
    [InverseProperty("Employees")]
    public virtual Persondetail? Detail { get; set; }

    [InverseProperty("Copilot")]
    public virtual ICollection<Flight> FlightCopilots { get; set; } = new List<Flight>();

    [InverseProperty("Pilot")]
    public virtual ICollection<Flight> FlightPilots { get; set; } = new List<Flight>();

    [InverseProperty("SupervisorPerson")]
    public virtual ICollection<Employee> InverseSupervisorPerson { get; set; } = new List<Employee>();

    [ForeignKey("SupervisorPersonId")]
    [InverseProperty("InverseSupervisorPerson")]
    public virtual Employee? SupervisorPerson { get; set; }
}
