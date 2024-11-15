using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Users;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints.Implementations;

public class UserEndpoint : IMessageEndpoints
{
    private readonly User _user;

    public UserEndpoint(User user)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
    }

    public void SendMessage(Message message)
    {
        _user.ReceivedMessage(message);
    }
}