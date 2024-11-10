using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class MessengerRecipient : IRecipient
{
    private readonly IMessenger _messenger;

    private readonly List<Message> _messages;

    private readonly ILogger _logger;

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public int MinimumImportanceLevel { get; }

    public MessengerRecipient(IMessenger messenger, int minimumImportanceLevel, ILogger logger)
    {
        _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        MinimumImportanceLevel = minimumImportanceLevel;
        _messages = new List<Message>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Receive(Message message)
    {
        if (message.ImportanceLevel >= MinimumImportanceLevel)
        {
            _messages.Add(message);
            _logger.Log($"MessengerRecipient получил сообщение ID: {message.Id}, Уровень важности: {message.ImportanceLevel}");
        }
    }

    public void Send(Message message)
    {
        _messenger.Print(message);
        _logger.Log($"MessengerRecipient отправил сообщение ID: {message.Id}");
    }
}