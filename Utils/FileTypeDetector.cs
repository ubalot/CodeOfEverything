using System.Collections.Generic;

namespace CodeOfEverything.Utils
{
    class FileTypeDetector
    {
        static readonly List<string> audioExtensions = [
            ".aif",
            ".cda",
            ".mid",
            ".mp3",
            ".mpa",
            ".ogg",
            ".wav",
            ".wma",
            ".wpl"
        ];

        static readonly List<string> fontExtensions = [
            ".fnt",
            ".fon",
            ".otf",
            ".ttf"
        ];

        static readonly List<string> imageExtensions = [
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
        ];

        static readonly List<string> videoExtensions = [
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
        ];

        internal static bool IsAudioFile(string fileExtension) => audioExtensions.Contains(fileExtension);

        internal static bool IsFontFile(string fileExtension) => fontExtensions.Contains(fileExtension);

        internal static bool IsImageFile(string fileExtension) => imageExtensions.Contains(fileExtension);

        internal static bool IsVideoFile(string fileExtension) => videoExtensions.Contains(fileExtension);
    }
}
