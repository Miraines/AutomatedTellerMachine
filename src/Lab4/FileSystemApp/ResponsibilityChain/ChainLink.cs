namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain;

public abstract class ChainLink<TRequest> : IChainLink<TRequest> where TRequest : class
{
    public IChainLink<TRequest>? Next { get; private set; }

    public IChainLink<TRequest> SetNext(IChainLink<TRequest> nextHandler)
    {
        Next = nextHandler;
        return nextHandler;
    }

    public abstract TRequest? Handle(TRequest request);
}