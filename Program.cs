﻿using System;
using System.Collections.Generic;
using CommandLine;

namespace CodeOfEverything
{
    [Verb("extract", HelpText = "Extract data")]
    class ExtractOptions
    {
        [Option("type", Required = true, HelpText = "Specify the type of data to extract.")]
        public string Type { get; set; }

        [Option("from", Required = true, HelpText = "Specify the source of the data.")]
        public string From { get; set; }
    }

    [Verb("format", HelpText = "Format data")]
    class FormatOptions
    {
        [Option('f', "file", Required = true, HelpText = "File with data")]
        public string File {get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<ExtractOptions, FormatOptions>(args)
                .WithParsed<ExtractOptions>(Extract)
                .WithParsed<FormatOptions>(Format)
                .WithNotParsed(HandleParseError);
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
        }

        static void Extract(ExtractOptions opts)
        {
            var type = opts.Type;
            var file = System.IO.Path.GetFullPath(opts.From);

            fileArgCheck(file);

            var dirPath = System.IO.Path.GetDirectoryName(file);
            var filename = System.IO.Path.GetFileName(file);
            var extractor = new Extractor.ExtractorFactory(file, type);
            extractor.execute();
        }

        static void Format(FormatOptions opts)
        {
            var file = System.IO.Path.GetFullPath(opts.File);

            fileArgCheck(file);

            var formatter = new SourceCodeFormatter.Formatter(file);
            formatter.execute();
        }

        private static void fileArgCheck(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException($"File doesn't exist: {filePath}");
            }
        }
    }
}