using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Extractor
{
    abstract class ExtractorBase
    {
        static readonly List<string> audioExtensions = new List<string>() {
            ".aif",
            ".cda",
            ".mid",
            ".mp3",
            ".mpa",
            ".ogg",
            ".wav",
            ".wma",
            ".wpl"
        };

        static readonly List<string> fontExtensions = new List<string>() {
            ".fnt",
            ".fon",
            ".otf",
            ".ttf"
        };

        static readonly List<string> imageExtensions = new List<string>() {
            ".ai",
            ".bmp",
            ".gif",
            ".ico",
            ".jpeg", ".jpg",
            ".png",
            ".ps",
            ".psd",
            ".svg",
            ".tif", ".tiff"
        };

        static readonly List<string> videoExtensions = new List<string>() {
            ".3g2",
            ".3gp",
            ".avi",
            ".flv",
            ".h264",
            ".m4v",
            ".mkv",
            ".mov",
            ".mp4",
            ".mpg", ".mpeg",
            ".rm",
            ".swf",
            ".vob",
            ".wmv"
        };

        protected static bool isAudioFile(string fileExtension)
        {
            return audioExtensions.Contains(fileExtension);
        }

        protected static bool isFontFile(string fileExtension)
        {
            return fontExtensions.Contains(fileExtension);
        }

        protected static bool isImageFile(string fileExtension)
        {
            return imageExtensions.Contains(fileExtension);
        }

        protected static bool isVideoFile(string fileExtension)
        {
            return videoExtensions.Contains(fileExtension);
        }

        abstract public void execute();
    }

    class WordDocExtractor : ExtractorBase
    {
        string type;
        string filePath;
        string zipFile;
        string destDir;

        public WordDocExtractor(string filePath_, string type_)
        {
            type = type_;
            if (type != "media")
            {
                Console.WriteLine($"invalid type: {type}");
                throw new InvalidOperationException("Invalid type");
            }

            filePath = filePath_;
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
                        if (isAudioFile(ext) || isFontFile(ext) || isImageFile(ext) || isVideoFile(ext))
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

        public LibreOfficeDocExtractor(string filePath_, string type_)
        {
            type = type_;
            if (type != "media")
            {
                Console.WriteLine($"invalid type: {type}");
                throw new InvalidOperationException("Invalid type");
            }

            filePath = filePath_;
            var dirPath = System.IO.Path.GetDirectoryName(filePath);
            var filename = System.IO.Path.GetFileName(filePath);
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
                        if (isAudioFile(ext) || isFontFile(ext) || isImageFile(ext) || isVideoFile(ext))
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

        public ExtractorFactory(string filePath_, string type_)
        {
            var filename = Path.GetFileName(filePath_);
            var fileExtension = Path.GetExtension(filename);
            if (fileExtension == ".doc" || fileExtension == ".docx")
            {
                extractor = new WordDocExtractor(filePath_, type_);
            }
            else if (fileExtension == ".odt")
            {
                extractor = new LibreOfficeDocExtractor(filePath_, type_);
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