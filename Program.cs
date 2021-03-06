﻿using System;
using McMaster.Extensions.CommandLineUtils;

namespace CodeOfEverything
{
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication {
                Name = "code-of-everything",
                Description = "CLI interface for code-of-everything. One day this program will do everything and even more!",
            };

            app.HelpOption(inherited: true);

            app.Command("extract", extractCmd => {
                Console.WriteLine("Extract subcommand");

                extractCmd.HelpOption();
                var optionType = extractCmd.Option<string>("-t|--type <TYPE>", "Target file type", CommandOptionType.SingleValue).IsRequired();
                var optionFile = extractCmd.Option<string>("-f|--from <FILE>", "Source file for extraction", CommandOptionType.SingleValue).IsRequired();

                extractCmd.OnExecute(() => {
                    var type = optionType.Value();
                    var file = System.IO.Path.GetFullPath(optionFile.Value());
                    Console.WriteLine($"Type: {type}, File: {file}");

                    if (!System.IO.File.Exists(file))
                    {
                        Console.WriteLine($"File doesn't exist: {file}");
                        return;
                    }

                    var dirPath = System.IO.Path.GetDirectoryName(file);
                    var filename = System.IO.Path.GetFileName(file);
                    var extractor = new Extractor.WordDocExtractor(file, type);
                    extractor.execute();
                });
            });

            return app.Execute(args);
        }
    }
}
