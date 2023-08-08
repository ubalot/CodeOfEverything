using System;
using System.IO;
using Utils;

namespace Cipher
{
    abstract class CipherBase
    {
        abstract public void execute();
    }

    class CipherFactory
    {
        CipherBase cipher;

        public CipherFactory(string type, string inputFilePath, string outputFilePath, string key, bool decrypt)
        {
            switch (type)
            {
                case "caesar":
                    cipher = new CaesarCipher(inputFilePath, outputFilePath, key, decrypt);
                    break;

                default:
                    Console.WriteLine($"invalid cipher: {type}");
                    throw new InvalidOperationException("Invalid cipher");
            }
        }

        public void execute()
        {
            cipher.execute();
        }
    }
}
