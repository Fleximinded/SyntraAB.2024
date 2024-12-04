using Syntra.Frituurtje.Contracts.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Contracts.Models
{
    public class MenuImage :ModelBase
    {
        public string Name { get; set; } = default!;
        public byte[]? Data { get; set; } = default!;
        public string? Url { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageType { get; set; }
        [NotMapped]
        public string ToImageSource { get => (Data?.Length > 0 && ImageType?.Length > 0) ? $"data:{ImageType};base64,{System.Convert.ToBase64String(Data)}" : ""; }
    }
}
