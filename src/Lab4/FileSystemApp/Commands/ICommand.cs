namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

public interface ICommand
{
    void Execute(FileSystemState state);
}