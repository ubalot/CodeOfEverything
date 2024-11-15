using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CodeOfEverything.Models.Arguments;

namespace CodeOfEverything.Services.Tasks.Cipher
{
    public class CipherFactory(
        IConsoleOutput console,
        ILogger<CipherFactory> logger,
        CipherOptions options) : ITaskFactory
    {
        private readonly IConsoleOutput _console = console;
        private readonly ILogger<CipherFactory> _logger = logger;
        private readonly CipherOptions _options = options;

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
