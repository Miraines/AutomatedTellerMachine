using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.FileHandles;

public class FileDeleteHandle : ChainLink<CommandRequest>
{
    private readonly IChainLink<CommandRequest> _nextHandler;
    private readonly FileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public FileDeleteHandle(IChainLink<CommandRequest> nextHandler, FileSystemState fileSystemState, IPrint printer)
    {
        _nextHandler = nextHandler;
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("file delete ", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        try
        {
            string[] parts = request.CommandName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string filePath = string.Join(" ", parts.Skip(2));

            var command = new FileDeleteCommand(filePath, request.Strategy ?? throw new InvalidOperationException());
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