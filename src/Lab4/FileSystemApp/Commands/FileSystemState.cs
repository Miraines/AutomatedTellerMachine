﻿namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

public class FileSystemState : IFileSystemState
{
    public string? CurrentPath { get; set; }
}