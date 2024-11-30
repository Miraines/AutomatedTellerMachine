namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

public class FileDeleteCommand : ICommand
{
    private readonly string _pathForFile;
    private readonly ICommandStrategy _strategy;

    public FileDeleteCommand(string pathForFile, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(IFileSystemState state)
    {
        _strategy.FileDeleteCommand(_pathForFile);
    }
}