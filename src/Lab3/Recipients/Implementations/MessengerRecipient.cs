using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints.Implementations;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;

public class MessengerRecipient : Recipient
{
    public MessengerRecipient(IMessenger messenger) : base(new MessengerEndpoint(messenger)) { }
}