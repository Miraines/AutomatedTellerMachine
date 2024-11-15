using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class Recipient : IRecipient
{
    private IMessageEndpoints MessageEndpoint { get; }

    public Recipient(IMessageEndpoints messageEndpoint)
    {
        MessageEndpoint = messageEndpoint ?? throw new ArgumentNullException(nameof(messageEndpoint));
    }

    public virtual void Receive(Message message)
    {
        MessageEndpoint.SendMessage(message);
    }
}