namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;

public class DisplayDriver : IDisplayDriver
{
    public void Clear()
    {
        Console.Clear();
    }

    public void WriteText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        Console.WriteLine(text);
    }
}