using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Fietshersteller.Db.Shared
{
    public abstract class DbBase
    {
        [MaxLength(100)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;   
        public bool IsDeleted { get; set; } = false;
    }
}
