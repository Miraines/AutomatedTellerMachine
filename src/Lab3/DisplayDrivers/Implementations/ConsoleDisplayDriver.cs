using Crayon;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers.Implementations;

public class ConsoleDisplayDriver : IDisplayDriver
{
    private readonly byte _r;

    private readonly byte _g;

    private readonly byte _b;

    public ConsoleDisplayDriver(byte r, byte g, byte b)
    {
        _r = r;
        _g = g;
        _b = b;
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void WriteText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        string coloredText = Output.Rgb(_r, _g, _b).Text(text);
        Console.WriteLine(coloredText);
    }
}