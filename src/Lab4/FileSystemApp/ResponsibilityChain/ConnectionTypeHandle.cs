using Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemApp.ResponsibilityChain;

public class ConnectionTypeHandle : ChainLink<CommandRequest>
{
    private const string LocalType = "local";

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.Equals("connect", StringComparison.OrdinalIgnoreCase) &&
            request.Mode?.Equals(LocalType, StringComparison.OrdinalIgnoreCase) == true)
        {
            if (Next != null)
            {
                return Next.Handle(request.WithStrategy(new LocalFileCommands()));
            }

            return request;
        }

        if (request.CommandName.Equals("disconnect", StringComparison.OrdinalIgnoreCase))
        {
            return request.WithStrategy(null).WithMode(null);
        }

        if (Next != null)
        {
            return Next.Handle(request);
        }

        return request;
    }
}