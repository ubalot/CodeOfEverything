using System;
using System.IO;
using System.IO.Compression;

namespace CodeOfEverything.src.Services.Tasks.Extractor
{
    class LibreOfficeDocExtractor : ExtractorBase
    {
        private readonly string type;
        private readonly string filePath;
        private readonly string zipFile;
        private readonly string destDir;

        public LibreOfficeDocExtractor(string filePath, string type)
        {
            this.type = type;
            this.filePath = filePath;

            var dirPath = Path.GetDirectoryName(this.filePath);
            var filename = Path.GetFileName(this.filePath);
            var fileTitle = Path.GetFileNameWithoutExtension(filename);
            var fileExtension = Path.GetExtension(filename);
            if (fileExtension != ".odt")
            {
                throw new InvalidOperationException($"Invalid extension: {fileExtension}");
            }

            zipFile = $"{fileTitle}.zip";
            destDir = Path.Combine(dirPath, $"media-{fileTitle}");
        }

        public override void Execute()
        {
            // copy LibreOffice document to Zip file
            File.Copy(filePath, zipFile, true);

            // create destDir
            if (!File.Exists(destDir))
                Directory.CreateDirectory(destDir);

            ExtractFiles();

            // delete Zip file
            File.Delete(zipFile);
        }

        private void ExtractFiles()
        {
            using ZipArchive zip = ZipFile.OpenRead(zipFile);
            foreach (ZipArchiveEntry entry in zip.Entries)
            {
                switch (type)
                {
                    case "media":
                        var ext = Path.GetExtension(entry.Name);
                        if (IsMediaFile(ext))
                        {
                            var destFile = Path.Combine(destDir, entry.Name);
                            try
                            {
                                entry.ExtractToFile(destFile);
                            }
                            catch (IOException)
                            {
                                Console.WriteLine($"File already exists: {destFile}");
                            }
                        }
                        break;
                    
                    default:
                        throw new InvalidOperationException($"Type not supported: {type}");
                }
            }
        }
    }
}
