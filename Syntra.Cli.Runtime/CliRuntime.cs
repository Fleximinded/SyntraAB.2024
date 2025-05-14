using Syntra.Cli.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Cli.Runtime
{
    public class CliRuntime : ICliRuntime
    {
        public bool DoExit { get; private set; }
        public string Cursor { get; set; } = "$";
        public void Exit() { DoExit = true; }
        public Dictionary<string, ICliExecutable> Executors { get; private set; } = [];
        public Dictionary<string,CliPluginContext> Plugins { get; private set; } = [];  
        CliConfig Config {  get; set; } = new CliConfig();
        string[] ICliRuntime.Plugins { get => Plugins.Keys.ToArray(); }

        public CliRuntime() :this(null)
        {
             
        }
        public CliRuntime(CliConfig? config)
        {
            Config = config ?? Config;
            AddExecutor(new BasicExecution());
            AddPlugins();
            Console.WriteLine();
        }

        private void AddPlugins()
        {
            if(!string.IsNullOrWhiteSpace(Config.PluginDirectory) && Directory.Exists(Config.PluginDirectory))
            {
                var pluginFiles = Directory.GetFiles(Config.PluginDirectory, "*.dll");
                foreach(var file in pluginFiles)
                {
                    try
                    {
                        CliPluginContext plugin = new CliPluginContext(file);
                        if(plugin.Plugin != null)
                        {
                            if(Executors.ContainsKey(plugin.Plugin.Name) == false)
                            {
                                AddExecutor(plugin.Plugin);
                                Plugins.Add(plugin.Plugin.Name, plugin);
                            }
                        }   
                    } catch(Exception ex)
                    {
                        Console.WriteLine($"Error loading plugin {file}: {ex.Message}");
                    }
                }
            }   

        }

        public bool AddExecutor(ICliExecutable executable)
        {
            if(!Executors.ContainsKey(executable.Name))
            {
                Executors.Add(executable.Name, executable);
                Console.WriteLine($"Executor {executable.Name} added");
                return true;
            }
            return false;
        }

        public void Execute()
        {
            while(!DoExit)
            {
                Console.Write(Cursor);
                var rawCmd = Console.ReadLine();
                var command = CliCommand.Parse(rawCmd);
                foreach( var exe in Executors.Values ) {
                    if(exe.Execute(this, command))
                    {
                        break;
                    }
                
                }
            }



        }

        public static void Run(string[]? args = null)
        {
            CliRuntime runtime = new CliRuntime();
            runtime.Execute();
        }


    }
}
