namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

public interface IFileSystemState
{
    public string? CurrentPath { get; set; }
}