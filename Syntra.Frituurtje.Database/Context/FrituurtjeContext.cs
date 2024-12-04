using Microsoft.EntityFrameworkCore;
using Syntra.Frituurtje.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Database.Context
{
    public class FrituurtjeContext :DbContext
    {
        public DbSet<MenuTopic> Topics { get; set; }
        public DbSet<MenuItem> Items { get; set; }
        public DbSet<MenuImage> Images { get; set; }

        public FrituurtjeContext(DbContextOptions<FrituurtjeContext> options):base(options) { }  
    }
}
