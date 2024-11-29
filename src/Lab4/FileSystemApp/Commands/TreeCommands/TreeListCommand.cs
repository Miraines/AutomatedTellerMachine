namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.TreeCommands;

public class TreeListCommand : ICommand
{
    private readonly ICommandStrategy _strategy;
    private readonly IPrint _printer;
    private int _depth = 1;

    public TreeListCommand(ICommandStrategy strategy, IPrint printer)
    {
        _strategy = strategy;
        _printer = printer;
    }

    public void SetDepth(int depth)
    {
        _depth = depth;
    }

    public void Execute(FileSystemState state)
    {
        _strategy.TreeListCommand(_depth, _printer);
    }
}