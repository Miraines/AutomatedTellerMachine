namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

public class LocalFileCommands : ICommandStrategy
{
    private string? _currentPath;

    public string? ConnectCommand(string? fullPath)
    {
        if (fullPath == null || !Directory.Exists(fullPath))
        {
            throw new ArgumentException($"Invalid path: {fullPath}");
        }

        _currentPath = fullPath;
        return _currentPath;
    }

    public string? Disconnect()
    {
        _currentPath = null;
        return _currentPath;
    }

    public void FileConsoleShowCommand(string pathForFile)
    {
        string fullFilePath = GetFullPath(pathForFile);

        if (!File.Exists(fullFilePath))
        {
            throw new FileNotFoundException($"File not found: {fullFilePath}");
        }

        try
        {
            string content = File.ReadAllText(fullFilePath);
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error reading file: {fullFilePath}", ex);
        }
    }

    public void FileCopyCommand(string pathForFile, string pathForDirectory)
    {
        string sourceFilePath = GetFullPath(pathForFile);
        string destinationDirectory = GetFullPath(pathForDirectory);

        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException($"Source file not found: {sourceFilePath}");
        }

        if (!Directory.Exists(destinationDirectory))
        {
            throw new DirectoryNotFoundException($"Destination directory not found: {destinationDirectory}");
        }

        try
        {
            string uniqueDestinationPath = GenerateUniqueFileName(destinationDirectory, Path.GetFileName(pathForFile));
            File.Copy(sourceFilePath, uniqueDestinationPath);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error copying file: {sourceFilePath}", ex);
        }
    }

    public void FileDeleteCommand(string pathForFile)
    {
        string fullPath = GetFullPath(pathForFile);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"File not found: {fullPath}");
        }

        try
        {
            File.Delete(fullPath);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error deleting file: {fullPath}", ex);
        }
    }

    public void FileMoveCommand(string pathForFile, string pathForDirectory)
    {
        string sourceFilePath = GetFullPath(pathForFile);
        string destinationDirectory = GetFullPath(pathForDirectory);

        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException($"Source file not found: {sourceFilePath}");
        }

        if (!Directory.Exists(destinationDirectory))
        {
            throw new DirectoryNotFoundException($"Destination directory not found: {destinationDirectory}");
        }

        try
        {
            string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(pathForFile));
            File.Move(sourceFilePath, destinationFilePath, true);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error moving file: {sourceFilePath}", ex);
        }
    }

    public void FileRenameCommand(string pathForFile, string newName)
    {
        string fullPath = GetFullPath(pathForFile);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"File not found: {fullPath}");
        }

        try
        {
            string directory = Path.GetDirectoryName(fullPath) ?? string.Empty;
            string extension = Path.GetExtension(fullPath);
            string newFileNameWithExtension = newName + extension;
            string newFilePath = Path.Combine(directory, newFileNameWithExtension);
            File.Move(fullPath, newFilePath, true);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error renaming file: {fullPath}", ex);
        }
    }

    public string? TreeGotoCommand(string? fullPath)
    {
        if (fullPath == null)
        {
            return _currentPath;
        }

        if (Path.IsPathRooted(fullPath))
        {
            if (!Directory.Exists(fullPath))
            {
                throw new ArgumentException($"Invalid path: {fullPath}");
            }

            _currentPath = fullPath;
        }
        else if (_currentPath != null)
        {
            string combinedPath = Path.Combine(_currentPath, fullPath);
            if (!Directory.Exists(combinedPath))
            {
                throw new ArgumentException($"Invalid path: {combinedPath}");
            }

            _currentPath = combinedPath;
        }
        else
        {
            throw new InvalidOperationException("Cannot use relative path without a current directory.");
        }

        return _currentPath;
    }

    public void TreeListCommand(int depth, IPrint print)
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            throw new InvalidOperationException("Not connected to a file system.");
        }

        if (!Directory.Exists(_currentPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {_currentPath}");
        }

        PrintTreeList(_currentPath, depth, print, " ");
    }

    private void PrintTreeList(string path, int depth, IPrint print, string indent)
    {
        if (depth < 0) return;

        PrintFiles(path, print, indent);
        PrintDirectories(path, depth, print, indent);
    }

    private void PrintFiles(string path, IPrint print, string indent)
    {
        string[] files;
        try
        {
            files = Directory.GetFiles(path);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"Access denied to directory: {path}", ex);
        }

        foreach (string file in files)
        {
            print.Print($"{indent}- {Path.GetFileName(file)}");
        }
    }

    private void PrintDirectories(string path, int depth, IPrint print, string indent)
    {
        string[] subDirs;
        try
        {
            subDirs = Directory.GetDirectories(path);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"Access denied to directory: {path}", ex);
        }

        foreach (string dir in subDirs)
        {
            print.Print($"{indent}+ {Path.GetFileName(dir)}");
            PrintTreeList(dir, depth - 1, print, indent + "   ");
        }
    }

    private string GenerateUniqueFileName(string directory, string fileNameWithExtension)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithExtension);
        string extension = Path.GetExtension(fileNameWithExtension);

        string newFileName = fileNameWithExtension;
        int counter = 1;

        while (File.Exists(Path.Combine(directory, newFileName)))
        {
            newFileName = $"{fileNameWithoutExtension}({counter++}){extension}";
        }

        return Path.Combine(directory, newFileName);
    }

    private string GetFullPath(string relativeOrAbsolutePath)
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            return relativeOrAbsolutePath;
        }

        return Path.IsPathRooted(relativeOrAbsolutePath)
            ? relativeOrAbsolutePath
            : Path.GetFullPath(Path.Combine(_currentPath, relativeOrAbsolutePath));
    }
}