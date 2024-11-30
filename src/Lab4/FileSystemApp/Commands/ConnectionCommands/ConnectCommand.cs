namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.ConnectionCommands;

public class ConnectCommand : ICommand
{
    private readonly string? _fullPath;
    private readonly ICommandStrategy _strategy;

    public ConnectCommand(string fullPath, ICommandStrategy strategy)
    {
        _fullPath = fullPath;
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(IFileSystemState state)
    {
        state.CurrentPath = _strategy.ConnectCommand(_fullPath);
    }
}