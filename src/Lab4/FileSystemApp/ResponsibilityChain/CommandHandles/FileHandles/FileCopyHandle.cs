using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.FileHandles;

public class FileCopyHandle : ChainLink<CommandRequest>
{
    private readonly IChainLink<CommandRequest> _nextHandler;
    private readonly FileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public FileCopyHandle(IChainLink<CommandRequest> nextHandler, FileSystemState fileSystemState, IPrint printer)
    {
        _nextHandler = nextHandler;
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("file copy ", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        try
        {
            string[] parts = request.CommandName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string sourcePath = parts[2];
            string destinationPath = string.Join(" ", parts.Skip(3));

            var command = new FileCopyCommand(
                sourcePath,
                destinationPath,
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