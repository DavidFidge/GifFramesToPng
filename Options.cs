using CommandLine;

public class Options
{
    [Option('d', "Directory", Required = true, HelpText = "Directory to convert")]
    public string? Directory { get; set; }

    [Option('o', "OverwriteExisting", HelpText = "Overwrite existing pngs")]
    public bool OverwriteExisting { get; set; } = true;
}