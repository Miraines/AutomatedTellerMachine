namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

public class ConsolePrint : IPrint
{
    public void Print(string printable) => Console.WriteLine(printable);
}