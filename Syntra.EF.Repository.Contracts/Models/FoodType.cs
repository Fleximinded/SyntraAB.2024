using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Repository.Contracts
{
    public class FoodType : ModelBase
    {
        public FoodType() { }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
