using System.Collections.Generic;

namespace CodeOfEverything.src.Utils
{
    class FileTypeDetector
    {
        static readonly List<string> audioExtensions = new() {
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

        static readonly List<string> fontExtensions = new() {
            ".fnt",
            ".fon",
            ".otf",
            ".ttf"
        };

        static readonly List<string> imageExtensions = new() {
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

        static readonly List<string> videoExtensions = new() {
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

        internal static bool IsAudioFile(string fileExtension)
        {
            return audioExtensions.Contains(fileExtension);
        }

        internal static bool IsFontFile(string fileExtension)
        {
            return fontExtensions.Contains(fileExtension);
        }

        internal static bool IsImageFile(string fileExtension)
        {
            return imageExtensions.Contains(fileExtension);
        }

        internal static bool IsVideoFile(string fileExtension)
        {
            return videoExtensions.Contains(fileExtension);
        }
    }
}
