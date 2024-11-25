using Microsoft.EntityFrameworkCore;
using Syntra.EF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Domain
{
    public class TimeRegistrationContext :DbContext
    {
        public DbSet<Person> Person { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<TimeRegistration> TimeRegistrations { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<TaskDescription> TaskDescriptions { get; set; } = default!;
        public TimeRegistrationContext()
        {
                
        }
        public TimeRegistrationContext(DbContextOptions options) :base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
                       //optionsBuilder.UseSqlite(@"Data source=D:\data\Syntra\DB\TimeRegistration.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskDescription>().HasData(
                new TaskDescription("Web design", "Fun with HTML and so", 85),
                new TaskDescription("Web development", "Fun with C# and so", 95),
                new TaskDescription("Writing documentation", "No fun at all...", 280));   
            modelBuilder.Entity<Employee>().HasData(
                new Employee("Doe", "John", new DateTime(1980, 1, 1)) { EmployeeNumber = "123", HireDate = new DateTime(2010, 1, 1) },
                new Employee("Doe", "Jane", new DateTime(1985, 1, 1)) { EmployeeNumber = "124", HireDate = new DateTime(2011, 1, 1) });
        }
    }
}
