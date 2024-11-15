using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CodeOfEverything.src.Models.Arguments;

namespace CodeOfEverything.src.Services.Tasks.Cipher
{
    public class CipherFactory : ITaskFactory
    {
        private readonly IConsoleOutput _console;
        private readonly ILogger<CipherFactory> _logger;
        private readonly CipherOptions _options;

        public CipherFactory(
            IConsoleOutput console,
            ILogger<CipherFactory> logger,
            CipherOptions options)
        {
            _console = console;
            _logger = logger;
            _options = options;
        }

        public Task<int> Launch() => Task.Run(() =>
        {
            _logger?.LogDebug("CipherFactory starts.");
            var cipher = _options.Type switch
            {
                "caesar" => new CaesarCipher(_options.Input, _options.Output, _options.Key, _options.Decrypt),
                _ => throw new InvalidOperationException($"invalid cipher: {_options.Type}"),
            };
            cipher.Execute();
            _logger?.LogDebug("CipherFactory ends.");
            return 0;
        });
    }
}
