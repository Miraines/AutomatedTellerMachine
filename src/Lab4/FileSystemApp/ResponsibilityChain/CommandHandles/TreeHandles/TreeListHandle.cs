using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.TreeCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.TreeHandles;

public class TreeListHandle : ChainLink<CommandRequest>
{
    private readonly IChainLink<CommandRequest> _nextHandler;
    private readonly FileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public TreeListHandle(IChainLink<CommandRequest> nextHandler, FileSystemState fileSystemState, IPrint printer)
    {
        _nextHandler = nextHandler;
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("tree list", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        try
        {
            var command = new TreeListCommand(request.Strategy ?? throw new InvalidOperationException(), _printer);
            if (request.Depth.HasValue)
            {
                command.SetDepth(request.Depth.Value);
            }

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