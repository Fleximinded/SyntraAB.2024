using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Repository.Contracts
{
    public class ModelBase
    {
        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
