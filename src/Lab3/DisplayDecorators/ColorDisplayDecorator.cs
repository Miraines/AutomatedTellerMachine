using Crayon;
using Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;
using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDecorators;

public class ColorDisplayDecorator : DisplayDecorator
{
    private readonly byte _r;
    private readonly byte _g;
    private readonly byte _b;

    public ColorDisplayDecorator(IDisplay display, IDisplayDriver displayDriver, byte r, byte g, byte b) : base(display)
    {
        _r = r;
        _g = g;
        _b = b;
    }

    public override void DisplayMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        string coloredDescription = Output.Rgb(_r, _g, _b).Text(message.Description);
        string coloredContent = Output.Rgb(_r, _g, _b).Text(message.Content);

        string coloredText = $"{coloredDescription}: {coloredContent}";

        if (Display is Display concreteDisplay)
        {
            concreteDisplay.WriteColoredText(coloredText);
        }
        else
        {
            base.DisplayMessage(message);
        }
    }
}