using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints.Implementations;

public class DisplayEndpoint : IMessageEndpoints
{
    private readonly IDisplay _display;

    public DisplayEndpoint(IDisplay display)
    {
        _display = display ?? throw new ArgumentNullException(nameof(display));
    }

    public void SendMessage(Message message)
    {
        _display.DisplayMessage(message);
    }
}