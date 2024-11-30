using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.ConnectionCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.ConnectionHandles;

public class ConnectHandle : ChainLink<CommandRequest>
{
    private readonly IFileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public ConnectHandle(IFileSystemState fileSystemState, IPrint printer)
    {
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.Equals("connect", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                string fullPath = request.Arguments[0];

                var command = new ConnectCommand(fullPath, request.Strategy ?? throw new InvalidOperationException());
                command.Execute(_fileSystemState);

                return request;
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