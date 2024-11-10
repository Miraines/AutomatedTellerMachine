using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDecorators;

public abstract class DisplayDecorator : IDisplay
{
    protected IDisplay Display { get; }

    protected DisplayDecorator(IDisplay display)
    {
        Display = display ?? throw new ArgumentNullException(nameof(display));
    }

    public virtual void DisplayMessage(Message message)
    {
        Display.DisplayMessage(message);
    }
}