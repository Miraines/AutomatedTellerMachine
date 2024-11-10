namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;

public class FileDisplayDriver : IDisplayDriver
{
    private readonly string _filePath;

    public FileDisplayDriver(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым.", nameof(filePath));
        }

        _filePath = filePath;
    }

    public void Clear()
    {
        File.WriteAllText(_filePath, string.Empty);
    }

    public void WriteText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        File.AppendAllText(_filePath, text + Environment.NewLine);
    }
}