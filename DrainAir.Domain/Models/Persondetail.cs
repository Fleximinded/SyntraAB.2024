using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain.Models;

[Table("Persondetail")]
public partial class Persondetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public string? Memo { get; set; }

    public byte[]? Photo { get; set; }

    [StringLength(30)]
    public string? Street { get; set; }

    [StringLength(30)]
    public string? City { get; set; }

    [StringLength(3)]
    public string? Country { get; set; }

    [StringLength(8)]
    public string? Postcode { get; set; }

    [StringLength(130)]
    public string? Planet { get; set; }

    [InverseProperty("Detail")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Detail")]
    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
