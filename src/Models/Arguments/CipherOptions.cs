using System.Collections.Generic;
using CommandLine;

namespace CodeOfEverything.src.Models.Arguments
{
    [Verb("cipher", HelpText = "Cipher/Decipher file with desired encryption technique")]
    public class CipherOptions
    {
        [Option("type", Required = true, HelpText = "Specify the encryption/decryption technique.")]
        public string Type { get; set; }

        [Option("input", Required = true, HelpText = "Specify the text that will be encrypted/decrypted.")]
        public string Input { get; set; }

        [Option("output", Required = true, HelpText = "Specify the destination of the encrypted/decrypted text.")]
        public string Output { get; set; }

        [Option("key", Required = true, HelpText = "Key to be used for encryption/decryption.")]
        public string Key { get; set; }

        [Option(Default = false, HelpText = "Encrypt or decrypt action.")]
        public bool Decrypt { get; set; }
    }
}