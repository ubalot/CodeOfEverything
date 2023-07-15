using System;
using System.IO;
using System.IO.Compression;
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
    class WordDocExtractor : ExtractorBase
    {
        string type;
        string filePath;
        string zipFile;
        string destDir;

        public WordDocExtractor(string filePath, string type)
        {
            this.type = type;
            this.filePath = filePath;

            var dirPath = System.IO.Path.GetDirectoryName(filePath);
            var filename = System.IO.Path.GetFileName(filePath);
            var fileTitle = Path.GetFileNameWithoutExtension(filename);
            var fileExtension = Path.GetExtension(filename);
            if (fileExtension != ".doc" && fileExtension != ".docx")
            {
                Console.WriteLine($"invalid extension: {fileExtension}");
                throw new InvalidOperationException("Invalid extension");
            }

            zipFile = $"{fileTitle}.zip";
            destDir = System.IO.Path.Combine(dirPath, $"media-{fileTitle}");
        }

        public override void execute()
        {
            // copy Word document to Zip file
            System.IO.File.Copy(filePath, zipFile, true);

            // create destDir
            if (!System.IO.File.Exists(destDir))
                System.IO.Directory.CreateDirectory(destDir);

            extractFiles();

            // delete Zip file
            System.IO.File.Delete(zipFile);
        }

        private void extractFiles()
        {
            using (ZipArchive zip = ZipFile.Open(zipFile, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    if (type == "media")
                    {
                        var ext = Path.GetExtension(entry.Name);
                        if (isMediaFile(ext))
                        {
                            var destFile = System.IO.Path.Combine(destDir, entry.Name);
                            try
                            {
                                entry.ExtractToFile(destFile);
                            }
                            catch (System.IO.IOException)
                            {
                                Console.WriteLine($"File already exists: {destFile}");
                            }
                        }
                    }
                }
            }
        }
    }

    class LibreOfficeDocExtractor : ExtractorBase
    {
        string type;
        string filePath;
        string zipFile;
        string destDir;

        public LibreOfficeDocExtractor(string filePath, string type)
        {
            this.type = type;
            this.filePath = filePath;

            var dirPath = System.IO.Path.GetDirectoryName(this.filePath);
            var filename = System.IO.Path.GetFileName(this.filePath);
            var fileTitle = Path.GetFileNameWithoutExtension(filename);
            var fileExtension = Path.GetExtension(filename);
            if (fileExtension != ".odt")
            {
                Console.WriteLine($"invalid extension: {fileExtension}");
                throw new InvalidOperationException("Invalid extension");
            }

            zipFile = $"{fileTitle}.zip";
            destDir = System.IO.Path.Combine(dirPath, $"media-{fileTitle}");
        }

        public override void execute()
        {
            // copy LibreOffice document to Zip file
            System.IO.File.Copy(filePath, zipFile, true);

            // create destDir
            if (!System.IO.File.Exists(destDir))
                System.IO.Directory.CreateDirectory(destDir);

            extractFiles();

            // delete Zip file
            System.IO.File.Delete(zipFile);
        }

        private void extractFiles()
        {
            using (ZipArchive zip = ZipFile.Open(zipFile, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    if (type == "media")
                    {
                        var ext = Path.GetExtension(entry.Name);
                        if (isMediaFile(ext))
                        {
                            var destFile = System.IO.Path.Combine(destDir, entry.Name);
                            try
                            {
                                entry.ExtractToFile(destFile);
                            }
                            catch (System.IO.IOException)
                            {
                                Console.WriteLine($"File already exists: {destFile}");
                            }
                        }
                    }
                }
            }
        }
    }

    class ExtractorFactory
    {
        ExtractorBase extractor;

        public ExtractorFactory(string filePath, string type)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
            }

            if (type != "media")
            {
                Console.WriteLine($"invalid type: {type}");
                throw new NotImplementedException("Invalid type");
            }

            var fileExtension = Path.GetExtension(filePath);
            if (fileExtension == ".doc" || fileExtension == ".docx")
            {
                extractor = new WordDocExtractor(filePath, type);
            }
            else if (fileExtension == ".odt")
            {
                extractor = new LibreOfficeDocExtractor(filePath, type);
            }
            else
            {
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