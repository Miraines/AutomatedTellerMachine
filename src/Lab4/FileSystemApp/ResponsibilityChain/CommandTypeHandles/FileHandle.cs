namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandTypeHandles;

public class FileHandle : ChainLink<CommandRequest>
{
    private readonly IChainLink<CommandRequest> _nextHandler;

    public FileHandle(IChainLink<CommandRequest> nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("file ", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        return _nextHandler?.Handle(request);
    }
}