using Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays;

public class Display : IDisplay
{
    private readonly IDisplayDriver _displayDriver;

    public Display(IDisplayDriver displayDriver)
    {
        _displayDriver = displayDriver ?? throw new ArgumentNullException(nameof(displayDriver));
    }

    public virtual void DisplayMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        _displayDriver.Clear();
        string coloredText = message.Description + ": " + message.Content;
        _displayDriver.WriteText(coloredText);
    }
}