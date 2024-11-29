namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.ConnectionCommands;

public class DisconnectCommand : ICommand
{
    private readonly ICommandStrategy _strategy;

    public DisconnectCommand(ICommandStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(FileSystemState state)
    {
        state.CurrentPath = _strategy.Disconnect();
    }
}