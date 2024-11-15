using Crayon;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers.Implementations;

public class FileDisplayDriver : IDisplayDriver
{
    private readonly string _filePath;
    private readonly byte _r;

    private readonly byte _g;

    private readonly byte _b;

    public FileDisplayDriver(string filePath, byte r, byte g, byte b)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым.", nameof(filePath));
        }

        _filePath = filePath;
        _r = r;
        _g = g;
        _b = b;
    }

    public void Clear()
    {
        File.WriteAllText(_filePath, string.Empty);
    }

    public void WriteText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        string coloredText = Output.Rgb(_r, _g, _b).Text(text);
        File.AppendAllText(_filePath, coloredText + Environment.NewLine);
    }
}