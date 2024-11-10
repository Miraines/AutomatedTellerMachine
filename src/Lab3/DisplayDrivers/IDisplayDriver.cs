namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;

public interface IDisplayDriver
{
    void Clear();

    void WriteText(string text);
}