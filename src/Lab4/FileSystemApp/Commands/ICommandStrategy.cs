namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

public interface ICommandStrategy
{
    string? ConnectCommand(string? fullPath);

    string? Disconnect();

    void FileConsoleShowCommand(string pathForFile);

    void FileCopyCommand(string pathForFile, string pathForDirectory);

    void FileDeleteCommand(string pathForFile);

    void FileMoveCommand(string pathForFile, string pathForDirectory);

    void FileRenameCommand(string pathForFile, string newName);

    string? TreeGotoCommand(string? fullPath);

    void TreeListCommand(int depth, IPrint print);
}