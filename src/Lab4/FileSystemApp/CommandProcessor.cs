using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp;

public class CommandProcessor
{
    private readonly CommandParser _parser;
    private readonly FileSystemState _fileSystemState;
    private readonly IPrint _printer;
    private bool _isRunning;

    public CommandProcessor(CommandParser parser, FileSystemState fileSystemState, IPrint printer)
    {
        _parser = parser;
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public void Run()
    {
        _isRunning = true;
        while (_isRunning)
        {
            _printer.Print("Enter the command (or 'exit' to quit):");
            string input = Console.ReadLine() ?? string.Empty;

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                _isRunning = false;
                break;
            }

            _parser.ParseAndExecute(input);

            _printer.Print($"Current Path: {_fileSystemState.CurrentPath}");
        }
    }
}