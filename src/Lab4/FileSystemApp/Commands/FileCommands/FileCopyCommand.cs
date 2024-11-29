﻿namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.FileCommands;

public class FileCopyCommand : ICommand
{
    private readonly string _pathForFile;
    private readonly string _pathForDirectory;
    private readonly ICommandStrategy _strategy;

    public FileCopyCommand(string pathForFile, string pathForDirectory, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _pathForDirectory = pathForDirectory ?? throw new ArgumentNullException(nameof(pathForDirectory));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(FileSystemState state)
    {
        _strategy.FileCopyCommand(_pathForFile, _pathForDirectory);
    }
}