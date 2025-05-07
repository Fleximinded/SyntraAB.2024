using Syntra.Cli.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Cli.Runtime
{
    public class CliPluginContext : AssemblyLoadContext
    {
        public string SourcePath { get; private set; }
        public ICliExecutable? Plugin { get; private set; }
        public CliPluginContext(string srcPath) :base("CliPluginContext", true) {
            SourcePath = srcPath;
            LoadPlugin(srcPath);
        }
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            // check if the assembly is already loaded in the default context   
            var defaultAssembly = Default.Assemblies.FirstOrDefault(a => a.FullName == assemblyName.FullName);
            if(defaultAssembly != null)
            {
                return defaultAssembly;
            }
            // else load the assembly from the specified path
            var assemblyPath = Path.Combine(Path.GetDirectoryName(SourcePath) ?? "", $"{assemblyName.Name}.dll");
            if(File.Exists(assemblyPath))
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }
        private bool LoadPlugin(string srcPath)
        {
            if(!string.IsNullOrWhiteSpace(srcPath) && File.Exists(srcPath))
            {
                var assembly = LoadFromAssemblyPath(srcPath);
                if(assembly != null)
                {
                    var types = assembly.GetTypes();
                    var pluginType=types.Where(t => typeof(ICliExecutable).IsAssignableFrom(t) && !t.IsAbstract).FirstOrDefault();
                    Plugin = pluginType != null ? Activator.CreateInstance(pluginType) as ICliExecutable : null;    
                    return Plugin != null;
                }
            }
            return false;
        }
        public void UnloadPlugin()
        {
            if(Plugin != null)
            {
                Plugin = null;
                Unload();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }
}
