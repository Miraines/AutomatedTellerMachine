namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

public class FileShowCommand : ICommand
{
    private readonly string _pathForFile;

    private readonly ICommandStrategy _strategy;

    public FileShowCommand(string pathForFile, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(FileSystemState state)
    {
        _strategy.FileConsoleShowCommand(_pathForFile);
    }
}