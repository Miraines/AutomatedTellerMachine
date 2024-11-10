using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class GroupRecipient : IRecipient
{
    public int MinimumImportanceLevel { get; }

    private readonly List<IRecipient> _subRecipients;

    private readonly ILogger _logger;

    public IReadOnlyList<IRecipient> SubRecipients => _subRecipients.AsReadOnly();

    public GroupRecipient(int minimumImportanceLevel, ILogger logger)
    {
        if (minimumImportanceLevel < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumImportanceLevel),
                "Минимальный уровень важности не может быть отрицательным.");
        }

        MinimumImportanceLevel = minimumImportanceLevel;
        _subRecipients = new List<IRecipient>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void AddSubRecipient(IRecipient recipient)
    {
        ArgumentNullException.ThrowIfNull(recipient);
        _subRecipients.Add(recipient);
        _logger.Log($"Добавлен подадресат в GroupRecipient. Общий уровень важности: {MinimumImportanceLevel}");
    }

    public void Receive(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (message.ImportanceLevel >= MinimumImportanceLevel)
        {
            _logger.Log(
                $"GroupRecipient получил сообщение ID: {message.Id}, Уровень важности: {message.ImportanceLevel}");
            foreach (IRecipient subRecipient in _subRecipients)
            {
                if (message.ImportanceLevel >= subRecipient.MinimumImportanceLevel)
                {
                    subRecipient.Receive(message);
                }
            }
        }
    }
}