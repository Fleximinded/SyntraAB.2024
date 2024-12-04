using Syntra.Frituurtje.Contracts.Shared;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Contracts.Models
{
    public class MenuItem :ModelBase
    {
        [StringLength(200)]
        public string Name { get; set; } = default!;
        [StringLength(2000)]
        public string? Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public List<MenuImage> Images { get; set; } = new List<MenuImage>();
        public MenuTopic Topic { get; set; } = default!;
    }
}
