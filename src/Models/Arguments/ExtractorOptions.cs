using System.Collections.Generic;
using CommandLine;

namespace CodeOfEverything.src.Models.Arguments
{
    [Verb("extract", HelpText = "Extract data from document")]
    public class ExtractorOptions
    {
        [Option("type", Required = true, HelpText = "Specify the type of data to extract.")]
        public string Type { get; set; }

        [Option("from", Required = true, HelpText = "Specify the source of the data.")]
        public string From { get; set; }
    }
}