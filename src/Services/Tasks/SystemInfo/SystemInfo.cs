using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CodeOfEverything.src.Models.Arguments;

namespace CodeOfEverything.src.Services.Tasks.SystemInfo
{
    public class SystemInfoFactory : ITaskFactory
    {
        private readonly IConsoleOutput _console;
        private readonly ILogger<SystemInfoFactory> _logger;
        private readonly SystemInfoFactoryOptions _options;

        public SystemInfoFactory(
            IConsoleOutput console,
            ILogger<SystemInfoFactory> logger,
            SystemInfoFactoryOptions options)
        {
            _console = console;
            _logger = logger;
            _options = options;
        }

        public Task<int> Launch() => Task.Run(() =>
        {
            try {
                _logger?.LogDebug("SystemInfo starts.");
                var platform = Environment.OSVersion.Platform;
                var command = platform switch
                {
                    PlatformID.Unix => _options.Info switch
                    {
                        "cpu" => "cat /proc/loadavg | cut -d' ' -f1",
                        "os" or null => "uname -s",
                        _ => throw new InvalidOperationException($"Invalid argument: {_options.Info}"),
                    },
                    _ => throw new NotImplementedException($"Unsopported platform: {platform}"),
                };
                var output = platform switch
                {
                    PlatformID.Unix => UnixProgramOutput(command),
                    _ => throw new NotImplementedException(),
                };
                Console.WriteLine(output);
                _logger?.LogDebug("SystemInfo ends.");
                return 0;
            }
            catch (Exception ex)
            {
                _console.WriteError($"Unable to get info: {ex.Message}");
                return -1;
            }
        });

        static protected string UnixProgramOutput(string command) {
            var startInfo = new ProcessStartInfo() {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
            }; 
            var process = new Process() { StartInfo = startInfo, };
            process.Start();
            process.WaitForExit(); // Wait for the child process to exit before reading to the end of its redirected stream.
            return process.StandardOutput.ReadToEnd();
        }
    }
}
