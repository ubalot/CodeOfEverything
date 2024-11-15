using System;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CodeOfEverything.Models.Arguments;
using CodeOfEverything.Utils;

namespace CodeOfEverything.Services.Tasks.Extractor
{
    abstract class ExtractorBase
    {
        abstract public void Execute();

        protected static bool IsMediaFile(string ext)
        {
            var isAudio = FileTypeDetector.IsAudioFile(ext);
            var isFont = FileTypeDetector.IsFontFile(ext);
            var isImage = FileTypeDetector.IsImageFile(ext);
            var isVideo = FileTypeDetector.IsVideoFile(ext);
            return isAudio || isFont || isImage || isVideo;
        }
    }

    public class ExtractorFactory(
        IConsoleOutput console,
        ILogger<ExtractorFactory> logger,
        ExtractorOptions options) : ITaskFactory
    {
        private readonly IConsoleOutput _console = console;
        private readonly ILogger<ExtractorFactory> _logger = logger;
        private readonly ExtractorOptions _options = options;

        public Task<int> Launch() => Task.Run(() =>
        {
            _logger?.LogDebug("ExtractorFactory starts.");
            var fileExtension = Path.GetExtension(_options.From);
            ExtractorBase extractor = fileExtension switch
            {
                ".doc" or ".docx" => new WordDocExtractor(_options.From, _options.Type),
                ".odt" => new LibreOfficeDocExtractor(_options.From, _options.Type),
                _ => throw new InvalidOperationException($"Invalid extension: {fileExtension}"),
            };
            extractor.Execute();
            _logger?.LogDebug("ExtractorFactory ends.");
            return 0;
        });
    }
}
