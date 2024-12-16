using Syntra.Frituurtje.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.UnitTests
{
    public static class FrituurtjeTestDb
    {
        public static FrituurtjeContext CreateContext() {
            var options = new DbContextOptionsBuilder<FrituurtjeContext>().UseInMemoryDatabase(databaseName: "FrituurtjeDb").Options;
            return new FrituurtjeContext(options);        
        }
        public static FrituurtjeContext FillTopicData(this FrituurtjeContext context, IEnumerable<MenuTopic> data, bool doSave = true)
        {
            context.Topics.AddRange(data);
            if(doSave) context.SaveChanges();
            return context;
        }
        public static FrituurtjeContext FillMenuItemData(this FrituurtjeContext context,IEnumerable<MenuItem> data,bool doSave = true)
        {
            context.Items.AddRange(data);
            if(doSave) context.SaveChanges();
            return context;
        }
    }
}
