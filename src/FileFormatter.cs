using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileFormatter
{
    class Formatter
    {
        string filePath;

        public Formatter(string filePath_)
        {
            filePath = filePath_;
        }

        public void execute()
        {
            lastLineIsEndline();
            removeTralingSpaces();
        }

        protected void lastLineIsEndline()
        {
            string text = File.ReadAllText(filePath);
            string[] lines = text.Split("\n");
            int originalLength = lines.Length;
            if (lines.Length == 0)
            {
                //ok
            }
            else if (lines.Length == 1)
            {
                if (lines[0].Length == 0)
                {
                    //ok
                }
                else
                {
                    //fix needed
                    lines.Append("");
                }
            }
            else
            {
                string lastLine = lines[lines.Length - 1];
                string preLastLine = lines[lines.Length - 2];
                if (lastLine.Length == 0 && preLastLine.Length == 0)
                {
                    //ok
                }
                else
                {
                    //fix needed
                    lines.Append("");
                }
            }

            if (originalLength != lines.Length)
            {
                string formattedText = string.Join("\n", lines);
                File.WriteAllText(filePath, formattedText);
            }
        }

        protected void removeTralingSpaces()
        {
            string text = File.ReadAllText(filePath);
            string[] lines = text.Split("\n");
            string[] result = Array.ConvertAll(lines, line => line.TrimEnd());
            string resultText = string.Join("\n", result);
            if (text.Length != resultText.Length)
            {
                File.WriteAllText(filePath, resultText);
            }
        }
    }
}
