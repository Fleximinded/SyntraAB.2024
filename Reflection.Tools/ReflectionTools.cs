using Syntra.Cli.Ext;

namespace Reflection.Tools
{
    public class ReflectionTools : ICliExecutable
    {
        public string Name { get => "Reflection.Tools"; }
        public string Description { get => "Reflection tools"; }

        public bool Execute(ICliRuntime owner, ICliCommand parameters)
        {
            switch(parameters.Command)
            {
                case "plugin.list":
                    foreach(var plugin in owner.Plugins) { 
                    Console.WriteLine($"=> {plugin}");
                    }
                    break;
               
                default:
                    return false;



            }
            return true;
        }
    }
}
