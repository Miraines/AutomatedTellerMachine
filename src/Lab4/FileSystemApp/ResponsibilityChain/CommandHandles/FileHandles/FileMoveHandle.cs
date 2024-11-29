using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.FileHandles;

public class FileMoveHandle : ChainLink<CommandRequest>
{
    private readonly IChainLink<CommandRequest> _nextHandler;
    private readonly FileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public FileMoveHandle(IChainLink<CommandRequest> nextHandler, FileSystemState fileSystemState, IPrint printer)
    {
        _nextHandler = nextHandler;
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("file move ", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        try
        {
            string[] parts = request.CommandName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string filePath = parts[2];
            string directoryPath = string.Join(" ", parts.Skip(3));

            var command = new FileMoveCommand(
                filePath,
                directoryPath,
                request.Strategy ?? throw new InvalidOperationException());
            command.Execute(_fileSystemState);
            return request;
        }
        catch (Exception ex)
        {
            _printer.Print($"Error: {ex.Message}");
            return request;
        }
    }
}