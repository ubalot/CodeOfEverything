﻿using System;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using CodeOfEverything.src.Models.Arguments;
using CodeOfEverything.src.Services;
using CodeOfEverything.src.Services.Tasks;
using CodeOfEverything.src.Services.Tasks.Cipher;
using CodeOfEverything.src.Services.Tasks.Extractor;
using CodeOfEverything.src.Services.Tasks.SourceCodeLinter;
using CodeOfEverything.src.Services.Tasks.SystemInfo;

namespace CodeOfEverything
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((context, services) =>
                    {
                        // Configure Serilog
                        Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(context.Configuration)
                            .MinimumLevel.Debug()
                            .WriteTo.Console()
                            .CreateLogger();

                        // Set up our console output class
                        services.AddSingleton<IConsoleOutput, ConsoleOutput>();

                        // Based upon verb/options, create services, including the task
                        var parserResult = Parser.Default.ParseArguments<CipherOptions, ExtractorOptions, SourceCodeLinterOptions, SystemInfoFactoryOptions>(args);
                        parserResult
                            .WithParsed<CipherOptions>(options =>
                            {
                                services.AddSingleton(options);
                                services.AddSingleton<ITaskFactory, CipherFactory>();
                            })
                            .WithParsed<ExtractorOptions>(options =>
                            {
                                services.AddSingleton(options);
                                services.AddSingleton<ITaskFactory, ExtractorFactory>();
                            })
                            .WithParsed<SystemInfoFactoryOptions>(options =>
                            {
                                services.AddSingleton(options);
                                services.AddSingleton<ITaskFactory, SystemInfoFactory>();
                            })
                            .WithParsed<SourceCodeLinterOptions>(options =>
                            {
                                services.AddSingleton(options);
                                services.AddSingleton<ITaskFactory, SourceCodeLinterFactory>();
                            });
                    })
                    .UseSerilog()
                    .Build();

                var task = host.Services.GetService<ITaskFactory>();
                return task == null
                    ? -1 // This can happen on --help or invalid arguments
                    : await task.Launch();
            }
            catch (Exception ex)
            {
                // Note that this should only occur if something went wrong with building Host
                await Console.Error.WriteLineAsync(ex.Message);
                return -1;
            }
        }
    }
}