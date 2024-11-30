using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.TreeCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.TreeHandles;

public class TreeGotoHandle : ChainLink<CommandRequest>
{
    private readonly IFileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public TreeGotoHandle(IFileSystemState fileSystemState, IPrint printer)
    {
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.StartsWith("tree goto", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                if (request.Arguments.Count > 0)
                {
                    string path = string.Join(" ", request.Arguments);
                    var command = new TreeGotoCommand(path, request.Strategy ?? throw new InvalidOperationException());
                    command.Execute(_fileSystemState);
                    return request;
                }
                else
                {
                    _printer.Print("Error: Missing path argument for 'tree goto'.");
                    return request;
                }
            }
            catch (Exception ex)
            {
                _printer.Print($"Error: {ex.Message}");
                return request;
            }
        }
        else if (Next != null)
        {
            return Next.Handle(request);
        }

        return null;
    }
}