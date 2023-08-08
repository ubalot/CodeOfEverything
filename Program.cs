﻿using System;
using System.Collections.Generic;
using CommandLine;

namespace CodeOfEverything
{
    [Verb("cipher", HelpText = "Cipher/Decipher file with desired encryption technique")]
    class CipherOptions
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

    [Verb("extract", HelpText = "Extract data")]
    class ExtractOptions
    {
        [Option("type", Required = true, HelpText = "Specify the type of data to extract.")]
        public string Type { get; set; }

        [Option("from", Required = true, HelpText = "Specify the source of the data.")]
        public string From { get; set; }
    }

    [Verb("format", HelpText = "Format data")]
    class FormatOptions
    {
        [Option('f', "file", Required = true, HelpText = "File with data")]
        public string File {get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<CipherOptions, ExtractOptions, FormatOptions>(args)
                .WithParsed<CipherOptions>(Cipher)
                .WithParsed<ExtractOptions>(Extract)
                .WithParsed<FormatOptions>(Format)
                .WithNotParsed(HandleParseError);
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
        }

        static void Cipher(CipherOptions opts)
        {
            var type = opts.Type;
            var inputFile = System.IO.Path.GetFullPath(opts.Input);
            var outputFile = System.IO.Path.GetFullPath(opts.Output);
            var key = opts.Key;
            var decrypt = opts.Decrypt;

            fileArgCheck(inputFile);

            var cipher = new Cipher.CipherFactory(type, inputFile, outputFile, key, decrypt);
            cipher.execute();
        }

        static void Extract(ExtractOptions opts)
        {
            var type = opts.Type;
            var file = System.IO.Path.GetFullPath(opts.From);

            fileArgCheck(file);

            var dirPath = System.IO.Path.GetDirectoryName(file);
            var filename = System.IO.Path.GetFileName(file);
            var extractor = new Extractor.ExtractorFactory(file, type);
            extractor.execute();
        }

        static void Format(FormatOptions opts)
        {
            var file = System.IO.Path.GetFullPath(opts.File);

            fileArgCheck(file);

            var formatter = new SourceCodeFormatter.Formatter(file);
            formatter.execute();
        }

        private static void fileArgCheck(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException($"File doesn't exist: {filePath}");
            }
        }
    }
}