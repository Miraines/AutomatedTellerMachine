using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints.Implementations;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;

public class DisplayRecipient : Recipient
{
    public DisplayRecipient(IDisplay display) : base(new DisplayEndpoint(display)) { }
}