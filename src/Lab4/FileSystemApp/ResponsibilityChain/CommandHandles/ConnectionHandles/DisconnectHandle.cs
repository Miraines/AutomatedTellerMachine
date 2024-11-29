using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands.ConnectionCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain.CommandHandles.ConnectionHandles;

public class DisconnectHandle : ChainLink<CommandRequest>
{
    private readonly FileSystemState _fileSystemState;
    private readonly IPrint _printer;

    public DisconnectHandle(FileSystemState fileSystemState, IPrint printer)
    {
        _fileSystemState = fileSystemState;
        _printer = printer;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.Equals("disconnect", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                var command = new DisconnectCommand(request.Strategy ?? throw new InvalidOperationException());
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

        return request;
    }
}