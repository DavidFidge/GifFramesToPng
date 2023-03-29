using GifFramesToPng;

var options = CommandLine.Parser.Default.ParseArguments<Options>(args).Value;

if (options == null)
{
    Console.WriteLine("Could not read command line options.");
    Environment.Exit(1);
}

var converter = new GifToPngConverter(options.OverwriteExisting);

var files = Directory.EnumerateFiles(options.Directory, "*.gif");

converter.ConvertFiles(files.Select(f => new FileInfo(f)));