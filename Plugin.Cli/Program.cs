using Syntra.Cli.Runtime;

namespace Plugin.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CliConfig config = new CliConfig() {  PluginDirectory= "D:\\data\\Syntra\\Plugins" };
            var runtime = new CliRuntime(config);
            runtime.Execute();
        }
    }
}
