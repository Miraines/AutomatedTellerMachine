using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp;

public class CommandParser
{
    private readonly IChainLink<CommandRequest> _firstHandler;
    private readonly IPrint _printer;

    public CommandParser(IChainLink<CommandRequest> firstHandler, IPrint printer)
    {
        _firstHandler = firstHandler;
        _printer = printer;
    }

    public void ParseAndExecute(string input)
    {
        try
        {
            var request = new CommandRequest(input);

            _firstHandler.Handle(request);
        }
        catch (ArgumentException ex)
        {
            _printer.Print($"Invalid command: {ex.Message}");
        }
        catch (Exception ex)
        {
            _printer.Print($"Error: {ex.Message}");
        }
    }
}