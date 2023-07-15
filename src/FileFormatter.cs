using System;
using System.IO;
using System.Linq;

namespace FileFormatter
{
    class Formatter
    {
        string filePath;

        public Formatter(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
            }

            this.filePath = filePath;
        }

        public void execute()
        {
            string text = File.ReadAllText(filePath);
            string formattedText = removeTralingSpaces(text);
            formattedText = ensureLastLineIsEmptyline(formattedText);
            if (text != formattedText)
            {
                File.WriteAllText(filePath, formattedText);
            }
        }

        protected string ensureLastLineIsEmptyline(string text)
        {
            if (text.Length == 0)
            {
                return text;
            }

            return text.TrimEnd('\r', '\n') + Environment.NewLine + Environment.NewLine;
        }

        protected string removeTralingSpaces(string text)
        {
            string[] lines = text.Split("\n");
            string[] result = Array.ConvertAll(lines, line => line.TrimEnd());
            return string.Join("\n", result);
        }
    }
}
