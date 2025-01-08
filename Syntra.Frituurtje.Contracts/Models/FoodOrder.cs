using Syntra.Frituurtje.Contracts.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Contracts.Models
{
    public class FoodOrder : ModelBase
    {
        [StringLength(50)]
        public string MenuItemId { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [StringLength(50)]
        public string ClientId { get; set; } = default!;
        public string MenuDescription { get; set; } = default!; 
        public DateTime OrderDate { get; set; }
        public string? Comment { get; set; }
    }
}
