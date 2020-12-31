using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace Extractor
{
    class WordDocExtractor
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
            var filename = System.IO.Path.GetFileName(filePath);
            var fileExtension = getFilenameExtension(filename);
            if (fileExtension != ".doc" && fileExtension != ".docx")
            {
                Console.WriteLine($"invalid extension: {fileExtension}");
                throw new InvalidOperationException("Invalid extension");
            }

            var idx = filePath.LastIndexOf(".");
            zipFile = $"{@filePath.Substring(0, idx)}.zip";

            var dirPath = System.IO.Path.GetDirectoryName(filePath);
            destDir = System.IO.Path.Combine(dirPath, "media");
        }

        public void execute()
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
                        var ext = getFilenameExtension(entry.Name);
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

        private static string getFilenameExtension(string filename)
        {
            var idx = filename.LastIndexOf(".");
            return filename.Substring(idx, filename.Length - idx);
        }

        static bool isAudioFile(string fileExtension)
        {
            var audioExtensions = new List<string>() {
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
            return audioExtensions.Contains(fileExtension);
        }

        static bool isFontFile(string fileExtension)
        {
            var fontExtensions = new List<string>() {
                ".fnt",
                ".fon",
                ".otf",
                ".ttf"
            };
            return fontExtensions.Contains(fileExtension);
        }

        static bool isImageFile(string fileExtension)
        {
            var imageExtensions = new List<string>() {
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
            return imageExtensions.Contains(fileExtension);
        }

        static bool isVideoFile(string fileExtension)
        {
            var videoExtensions = new List<string>() {
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
            return videoExtensions.Contains(fileExtension);
        }
    }
}