using CommandLine;

namespace CodeOfEverything.Models.Arguments
{
    [Verb("format", HelpText = "Format data")]
    public class SourceCodeLinterOptions
    {
        [Option('f', "file", Required = true, HelpText = "File with data")]
        public string File { get; set; }
    }
}