namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain;

public interface IChainLink<TRequest> where TRequest : class
{
    IChainLink<TRequest> SetNext(IChainLink<TRequest> nextHandler);

    TRequest? Handle(TRequest request);
}