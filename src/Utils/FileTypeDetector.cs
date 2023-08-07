using System.Collections.Generic;

namespace Utils
{
    class FileTypeDetector
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

        internal static bool isAudioFile(string fileExtension)
        {
            return audioExtensions.Contains(fileExtension);
        }

        internal static bool isFontFile(string fileExtension)
        {
            return fontExtensions.Contains(fileExtension);
        }

        internal static bool isImageFile(string fileExtension)
        {
            return imageExtensions.Contains(fileExtension);
        }

        internal static bool isVideoFile(string fileExtension)
        {
            return videoExtensions.Contains(fileExtension);
        }
    }
}
