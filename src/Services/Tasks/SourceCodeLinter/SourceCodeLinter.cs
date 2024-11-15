using System;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CodeOfEverything.src.Models.Arguments;

namespace CodeOfEverything.src.Services.Tasks.SourceCodeLinter
{
    public class SourceCodeLinterFactory : ITaskFactory
    {
        private readonly IConsoleOutput _console;
        private readonly ILogger<SourceCodeLinterFactory> _logger;
        private readonly SourceCodeLinterOptions _options;

        public SourceCodeLinterFactory(
            IConsoleOutput console,
            ILogger<SourceCodeLinterFactory> logger,
            SourceCodeLinterOptions options)
        {
            _console = console;
            _logger = logger;
            _options = options;
        }

        public Task<int> Launch() => Task.Run(() =>
        {
            _logger?.LogDebug("SourceCodeLinterFactory starts.");
            var filePath = _options.File;
            var text = File.ReadAllText(filePath);
            var formattedText = Format(text);
            if (text != formattedText)
            {
                File.WriteAllText(filePath, formattedText);
            }
            _logger?.LogDebug("SourceCodeLinterFactory ends.");
            return 0;
        });

        public static string Format(string text)
        {
            var formattedText = RemoveTralingSpaces(text);
            formattedText = EnsureLastLineIsEmptyline(formattedText);
            return formattedText;
        }

        protected static string EnsureLastLineIsEmptyline(string text)
        {
            var trimmedText = text.TrimEnd('\r', '\n');
            if (string.IsNullOrEmpty(trimmedText))
            {
                return trimmedText + Environment.NewLine;
            }
            else
            {
                return trimmedText + Environment.NewLine + Environment.NewLine;
            }
        }

        protected static string RemoveTralingSpaces(string text)
        {
            var lines = text.Split("\n");
            var result = Array.ConvertAll(lines, line => line.TrimEnd());
            return string.Join("\n", result);
        }
    }
}
