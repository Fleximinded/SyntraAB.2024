using Syntra.EF.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Data.Models
{
    public class TimeRegistration :DbBase
    {
        public TimeRegistration() { }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TaskDescription Description { get; set; } = default!;
        public Client ClientInfo { get; set; } = default!;
    }
}
