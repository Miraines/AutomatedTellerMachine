using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Users;

public class User
{
    public string Name { get; }

    private readonly List<ReceivedMessage> _receivedMessages;

    public IReadOnlyCollection<ReceivedMessage> Messages => _receivedMessages.AsReadOnly();

    public User(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _receivedMessages = new List<ReceivedMessage>();
    }

    public void ReceivedMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        var recievedMessage = new ReceivedMessage(message);
        _receivedMessages.Add(recievedMessage);
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
            Console.WriteLine($"Сообщение с ID {messageId} не найдено.");
            return;
        }

        receivedMessage.MarkAsRead();
    }
}