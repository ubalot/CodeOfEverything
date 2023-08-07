using System;
using System.IO;
using System.IO.Compression;

namespace Extractor
{
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
            using (ZipArchive zip = ZipFile.OpenRead(zipFile))
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
}
