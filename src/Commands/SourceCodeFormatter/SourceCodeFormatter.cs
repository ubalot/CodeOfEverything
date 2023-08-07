using System;
using System.IO;

namespace SourceCodeFormatter
{
    class Formatter
    {
        string filePath;

        public Formatter(string filePath)
        {
            this.filePath = filePath;
        }

        public void execute()
        {
            var text = File.ReadAllText(filePath);
            var formattedText = format(text);
            if (text != formattedText)
            {
                File.WriteAllText(filePath, formattedText);
            }
        }

        public static string format(string text)
        {
            var formattedText = removeTralingSpaces(text);
            formattedText = ensureLastLineIsEmptyline(formattedText);
            return formattedText;
        }

        protected static string ensureLastLineIsEmptyline(string text)
        {
            var trimmedText = text.TrimEnd('\r', '\n');
            if (string.IsNullOrEmpty(trimmedText))
            {
                return trimmedText + Environment.NewLine;
            }
            else
            {
                return trimmedText + Environment.NewLine + Environment.NewLine;
            }
        }

        protected static string removeTralingSpaces(string text)
        {
            var lines = text.Split("\n");
            var result = Array.ConvertAll(lines, line => line.TrimEnd());
            return string.Join("\n", result);
        }
    }
}
