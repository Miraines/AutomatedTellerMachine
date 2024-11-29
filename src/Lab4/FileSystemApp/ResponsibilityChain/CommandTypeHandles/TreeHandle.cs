namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandTypeHandles;

public class TreeHandle : ChainLink<CommandRequest>
{
    private readonly IChainLink<CommandRequest> _nextHandler;

    public TreeHandle(IChainLink<CommandRequest> nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("tree ", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        return _nextHandler?.Handle(request);
    }
}