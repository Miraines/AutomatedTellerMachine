namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.TreeCommands;

public class TreeGotoCommand : ICommand
{
    private readonly string? _fullPath;
    private readonly ICommandStrategy _strategy;

    public TreeGotoCommand(string fullPath, ICommandStrategy strategy)
    {
        _fullPath = fullPath;
        _strategy = strategy;
    }

    public void Execute(FileSystemState state)
    {
        state.CurrentPath = _strategy.TreeGotoCommand(_fullPath);
    }
}