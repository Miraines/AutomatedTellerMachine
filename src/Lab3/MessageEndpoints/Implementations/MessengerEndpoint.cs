using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints.Implementations;

public class MessengerEndpoint : IMessageEndpoints
{
    private readonly IMessenger _messenger;

    public MessengerEndpoint(IMessenger messenger)
    {
        _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
    }

    public void SendMessage(Message message)
    {
        _messenger.Print(message);
    }
}