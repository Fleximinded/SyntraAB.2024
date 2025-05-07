using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Cli.Runtime
{
    public class CliConfig
    {
        public string PluginDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "Plugins");
    }
}
