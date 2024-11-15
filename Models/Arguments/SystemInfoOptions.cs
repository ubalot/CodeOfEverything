using CommandLine;

namespace CodeOfEverything.Models.Arguments
{
    [Verb("sysinfo", HelpText = "System info")]
    public class SystemInfoFactoryOptions
    {
        [Option("info", Required = false, HelpText = "System info")]
        public string Info { get; set; }
    }
}