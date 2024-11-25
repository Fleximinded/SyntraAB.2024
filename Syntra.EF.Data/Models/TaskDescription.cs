using Syntra.EF.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Data.Models
{
    public class TaskDescription :DbBase
    {
        public TaskDescription() { }
        public TaskDescription(string name, string description, decimal hPrice)
        {
            Name = name;
            Description = description; 
            HourPrice = hPrice;
        }
        [StringLength(250)]
        public string Name { get; set; } = "";  
        [StringLength(4000)]
        public string? Description { get; set; }
        public decimal? HourPrice { get; set; }    
    }
}
