using System;
using System.Collections.Generic;
using DrainAir.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DrainAir.Domain;

public partial class DrainAirContext : DbContext
{
    public DrainAirContext()
    {
    }

    public DrainAirContext(DbContextOptions<DrainAirContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AircraftType> AircraftTypes { get; set; }

    public virtual DbSet<AircraftTypeDetail> AircraftTypeDetails { get; set; }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<DepartureGrouping> DepartureGroupings { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Persondetail> Persondetails { get; set; }

    public virtual DbSet<VDepartureStatistic> VDepartureStatistics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,22133;Initial Catalog=DrainAir;Persist Security Info=True;User ID=sa;Password=S03pM3tBall3k3s!;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.Property(e => e.FlightNo).ValueGeneratedNever();
            entity.Property(e => e.Departure).HasDefaultValue("(not set)");
            entity.Property(e => e.Destination).HasDefaultValue("(not set)");
            entity.Property(e => e.FlightDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasDefaultValue(12345m);
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Utilization).HasComputedColumnSql("((100.0)-(([FreeSeats]*(1.0))/[Seats])*(100.0))", false);

            entity.HasOne(d => d.Pilot).WithMany(p => p.FlightPilots).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.Passengers).WithMany(p => p.FlightNos)
                .UsingEntity<Dictionary<string, object>>(
                    "Booking",
                    r => r.HasOne<Passenger>().WithMany().HasForeignKey("PassengerId"),
                    l => l.HasOne<Flight>().WithMany().HasForeignKey("FlightNo"),
                    j =>
                    {
                        j.HasKey("FlightNo", "PassengerId");
                        j.ToTable("Booking");
                        j.HasIndex(new[] { "PassengerId" }, "IX_Booking_PassengerID");
                        j.IndexerProperty<int>("PassengerId").HasColumnName("PassengerID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
