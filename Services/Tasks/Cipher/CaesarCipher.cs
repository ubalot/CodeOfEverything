using System;
using System.IO;
using System.Linq;

namespace CodeOfEverything.Services.Tasks.Cipher
{
    class CaesarCipher
    {
        public static readonly int alphabetLenght = 'z' - 'a';

        private readonly string inputFile;
        private readonly string outputFile;
        private readonly char key;
        private readonly bool decrypt;

        public CaesarCipher(string inputFile, string outputFile, string key, bool decrypt)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                throw new ArgumentException($"'{nameof(inputFile)}' cannot be null or empty.", nameof(inputFile));
            }

            if (string.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentException($"'{nameof(outputFile)}' cannot be null or empty.", nameof(outputFile));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));
            }

            if (key.Length != 1)
            {
                throw new ArgumentException("key must be of length 1");
            }
            var k = key.ToCharArray()[0];
            if (!char.IsLetter(k))
            {
                throw new ArgumentException("key must be a ASCII character");
            }
            this.inputFile = inputFile;
            this.outputFile = outputFile;
            this.key = k;
            this.decrypt = decrypt;
        }

        public CaesarCipher(string inputFile, string outputFile, char key, bool decrypt) : this(inputFile, outputFile, key.ToString(), decrypt)
        {
        }

        public void Execute()
        {
            var data = string.Join(null, File.ReadAllText(inputFile).Select(c => char.IsLetter(c) ? Translate(c) : c));
            File.WriteAllText(outputFile, data);
        }

        protected char Translate(char c)
        {
            return decrypt ? Decipher(c) : Cipher(c);
        }

        protected char Cipher(char c)
        {
            return ShiftedChar(c, AsciiOffset(key));
        }

        protected char Decipher(char c)
        {
            return ShiftedChar(c, alphabetLenght - AsciiOffset(key));
        }

        protected static char ShiftedChar(char c, int shift)
        {
            var charIdx = AsciiOffset(c);
            var offset = (charIdx + shift) % alphabetLenght;
            try
            {
                return Convert.ToChar(AsciiLetterStartCode(c) + offset);
            }
            catch (OverflowException)
            {
                Console.WriteLine($"offset {offset} is out of range");
                throw;
            }
        }

        protected static int AsciiOffset(char c)
        {
            return c - AsciiLetterStartCode(c);
        }

        protected static int AsciiLetterStartCode(char c)
        {
            return char.IsLower(c) ? 'a' : 'A';
        }
    }
}