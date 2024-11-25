using Syntra.EF.Data.Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Data.Base
{
    public class DbBase : IDbBase
    {
        public DbBase() { Id = Guid.NewGuid().ToString("N"); }
        public DbBase(string key) { Id = key; }
        [MaxLength(30)]
        public string Id { get; set; }
    }
}
