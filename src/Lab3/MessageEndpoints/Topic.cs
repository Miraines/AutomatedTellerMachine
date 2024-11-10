using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;

public class Topic
{
    public string TopicName { get; }

    private readonly List<IRecipient> _recipients;

    private readonly ILogger _logger;

    public IReadOnlyList<IRecipient> Recipients => _recipients.AsReadOnly();

    public Topic(string topicName, ILogger logger)
    {
        if (string.IsNullOrWhiteSpace(topicName))
        {
            throw new ArgumentException($"'{nameof(topicName)}' cannot be null or whitespace.", nameof(topicName));
        }

        TopicName = topicName;
        _recipients = new List<IRecipient>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
        _logger.Log($"Добавлен адресат в Topic '{TopicName}'.");
    }

    public void Send(Message message)
    {
        _logger.Log(
            $"Отправка сообщения ID: {message.Id}, Уровень важности: {message.ImportanceLevel} через Topic '{TopicName}'.");
        foreach (IRecipient recipient in _recipients)
        {
            if (message.ImportanceLevel >= recipient.MinimumImportanceLevel)
            {
                recipient.Receive(message);
            }
        }

        _logger.Log($"Сообщение ID: {message.Id} отправлено всем соответствующим адресатам.");
    }
}