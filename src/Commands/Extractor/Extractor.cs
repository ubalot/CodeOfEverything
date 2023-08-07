using System;
using System.IO;
using Utils;

namespace Extractor
{
    abstract class ExtractorBase
    {
        abstract public void execute();

        protected bool isMediaFile(string ext)
        {
            var isAudio = FileTypeDetector.isAudioFile(ext);
            var isFont = FileTypeDetector.isFontFile(ext);
            var isImage = FileTypeDetector.isImageFile(ext);
            var isVideo = FileTypeDetector.isVideoFile(ext);
            return isAudio || isFont || isImage || isVideo;
        }
    }

    class ExtractorFactory
    {
        ExtractorBase extractor;

        public ExtractorFactory(string filePath, string type)
        {
            if (type != "media")
            {
                Console.WriteLine($"invalid type: {type}");
                throw new NotImplementedException("Invalid type");
            }

            var fileExtension = Path.GetExtension(filePath);
            switch (fileExtension)
            {
                case ".doc":
                case ".docx":
                    extractor = new WordDocExtractor(filePath, type);
                    break;

                case ".odt":
                    extractor = new LibreOfficeDocExtractor(filePath, type);
                    break;

                default:
                    Console.WriteLine($"invalid extension: {fileExtension}");
                    throw new InvalidOperationException("Invalid extension");
            }
        }

        public void execute()
        {
            extractor.execute();
        }
    }
}
