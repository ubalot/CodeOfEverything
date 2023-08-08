using System;
using System.IO;

namespace Cipher
{
    class CaesarCipher : CipherBase
    {
        string inputFile;
        string outputFile;
        char key;
        bool decrypt;

        public CaesarCipher(string inputFile, string outputFile, string key, bool decrypt)
        {
            this.inputFile = inputFile;
            this.outputFile = outputFile;
            if (key.Length > 1)
            {
                throw new ArgumentException("key must be of length 1");
            }
            this.key = key.ToCharArray()[0];
            if (!Char.IsAscii(this.key))
            {
                throw new ArgumentException("key must be a ASCII character");
            }
            this.decrypt = decrypt;
        }

        public override void execute()
        {
            var data = File.ReadAllText(inputFile);
            var output = "";
            foreach (var c in data)
            {
                if (Char.IsLetter(c))
                {
                    char shiftedChar;
                    if (!decrypt)
                    {
                        shiftedChar = cipher(c);
                    }
                    else
                    {
                        shiftedChar = decipher(c);
                    }
                    output += shiftedChar.ToString();
                }
            }
            File.WriteAllText(outputFile, output);
        }

        protected char cipher(char c)
        {
            var start = Char.IsLower(c) ? 'a' : 'A';
            var shiftedChar = ((c - start) + keyShift()) % 26;
            return Convert.ToChar(start + shiftedChar);
        }

        protected char decipher(char c)
        {
            var start = Char.IsLower(c) ? 'a' : 'A';
            var shiftedChar = ((c - start) + (26 - keyShift())) % 26;
            return Convert.ToChar(start + shiftedChar);
        }

        protected int keyShift()
        {
            return key - 'a';
        }
    }
}