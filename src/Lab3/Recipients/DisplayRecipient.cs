using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class DisplayRecipient : IRecipient
{
    private readonly IDisplay _display;
    private readonly ILogger _logger;

    public int MinimumImportanceLevel { get; }

    public DisplayRecipient(IDisplay display, int minimumImportanceLevel, ILogger logger)
    {
        _display = display ?? throw new ArgumentNullException(nameof(display));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (minimumImportanceLevel < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumImportanceLevel),
                "Минимальный уровень важности не может быть отрицательным.");
        }

        MinimumImportanceLevel = minimumImportanceLevel;
    }

    public void Receive(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (message.ImportanceLevel >= MinimumImportanceLevel)
        {
            _display.DisplayMessage(message);
            _logger.Log(
                $"DisplayRecipient получил сообщение ID: {message.Id}, Уровень важности: {message.ImportanceLevel}");
        }
    }
}