using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;

public class User
{
    public string Name { get; }

    private readonly List<ReceivedMessage> _receivedMessages;

    private readonly ILogger _logger;

    public IReadOnlyCollection<ReceivedMessage> Messages => _receivedMessages.AsReadOnly();

    public User(string name, ILogger logger)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
        _receivedMessages = new List<ReceivedMessage>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void ReceivedMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        var recievedMessage = new ReceivedMessage(message);
        _receivedMessages.Add(recievedMessage);
        _logger.Log($"Пользователь {Name} получил сообщение ID: {message.Id}, Описание: {message.Description}");
    }

    public void GetAllMessages()
    {
        foreach (ReceivedMessage receivedMessage in _receivedMessages)
        {
            Console.WriteLine($"ID: {receivedMessage.Message.Id}");
            Console.WriteLine($"Заголовок: {receivedMessage.Message.Description}");
            Console.WriteLine($"Содержимое: {receivedMessage.Message.Content}");
            Console.WriteLine($"Уровень важности: {receivedMessage.Message.ImportanceLevel}");
            Console.WriteLine($"Статус: {receivedMessage.Status}");
            Console.WriteLine(new string('-', 40));
        }
    }

    public void MarkMessagesAsRead(Guid messageId)
    {
        ReceivedMessage? receivedMessage = _receivedMessages.FirstOrDefault(m => m.Message.Id == messageId);

        if (receivedMessage == null)
        {
            _logger.Log(
                $"Попытка отметить как прочитанное несуществующее сообщение ID: {messageId} для пользователя {Name}.");
            return;
        }

        receivedMessage.MarkAsRead();
        _logger.Log($"Пользователь {Name} отметил сообщение ID: {messageId} как прочитанное.");
    }
}