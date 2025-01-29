using Syntra.Frituurtje.Contracts.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Contracts.Models
{
    public class MenuTopic :ModelBase
    {
        [StringLength(200)]
        public string Title { get; set; } = default!;
        [StringLength(2000)]
        public string? Description { get; set; }
        [JsonIgnore]
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
