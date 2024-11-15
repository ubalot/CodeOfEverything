using System;
using Microsoft.Extensions.Logging;

namespace CodeOfEverything.src.Services
{
    public class ConsoleOutput(ILogger<ConsoleOutput> logger) : IConsoleOutput
    {
        private readonly ILogger<ConsoleOutput> _logger = logger;

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
            _logger.LogInformation("{Message}", message);
        }

        public void WriteError(string message)
        {
            Console.Error.WriteLine(message);
            _logger.LogError("{Message}", message);
        }
    }

    public interface IConsoleOutput
    {
        void WriteLine(string message);
        void WriteError(string message);
    }
}