﻿namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

public class FileRenameCommand : ICommand
{
    private readonly string _pathForFile;
    private readonly string _newName;
    private readonly ICommandStrategy _strategy;

    public FileRenameCommand(string pathForFile, string newName, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _newName = newName ?? throw new ArgumentNullException(nameof(newName));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(FileSystemState state)
    {
        _strategy.FileRenameCommand(_pathForFile, _newName);
    }
}