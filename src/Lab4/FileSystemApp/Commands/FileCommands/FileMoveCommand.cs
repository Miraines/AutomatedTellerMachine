namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

public class FileMoveCommand : ICommand
{
    private readonly string _pathForFile;
    private readonly string _pathForDirectory;
    private readonly ICommandStrategy _strategy;

    public FileMoveCommand(string pathForFile, string pathForDirectory, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _pathForDirectory = pathForDirectory ?? throw new ArgumentNullException(nameof(pathForDirectory));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(IFileSystemState state)
    {
        _strategy.FileMoveCommand(_pathForFile, _pathForDirectory);
    }
}