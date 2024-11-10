namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}