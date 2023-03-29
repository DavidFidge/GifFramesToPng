using System.Windows.Media.Imaging;

namespace GifFramesToPng;

public class GifToPngConverter
{
    private readonly bool _overwriteExisting;

    public GifToPngConverter(bool overwriteExisting = true)
    {
        _overwriteExisting = overwriteExisting;
    }

    public void ConvertFiles(IEnumerable<FileInfo> files)
    {
        foreach (var file in files)
        {
            if (!File.Exists(file.FullName))
            {
                Console.WriteLine($"File does not exist: {file}");
            }
            else
            {
                ConvertFile(file);
            }
        }
    }

    public void ConvertFile(FileInfo file)
    {
        using var fileStream = file.Open(FileMode.Open);

        var gifBitmapDecoder = new GifBitmapDecoder(fileStream,
            BitmapCreateOptions.None, BitmapCacheOption.Default);

        var frameNumber = 1;
        foreach (var frame in gifBitmapDecoder.Frames)
        {
            var pngBitmapEncoder = new PngBitmapEncoder();

            var outFilename = Path.Join(file.Directory.FullName, $"{Path.GetFileNameWithoutExtension(file.Name)}_{frameNumber}.png");

            var fileExists = File.Exists(outFilename);

            if (_overwriteExisting && fileExists)
            {
                File.Delete(outFilename);
            }
            else if (fileExists)
            {
                Console.WriteLine($"Skipping file {file.FullName} as png already exists and overwrite is disabled: {outFilename}");
                continue;
            }

            using var outFile = File.Create(outFilename);

            pngBitmapEncoder.Frames.Add(frame);

            pngBitmapEncoder.Save(outFile);

            outFile.Close();

            Console.WriteLine($"Converted file {file.FullName} frame {frameNumber} to {outFilename}");

            frameNumber++;
        }
    }
}