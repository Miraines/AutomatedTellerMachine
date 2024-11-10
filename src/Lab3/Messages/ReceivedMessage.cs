namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public enum MessageStatus
{
    Unread,
    Read,
}

public class ReceivedMessage
{
    public Message Message { get; }

    public MessageStatus Status { get; private set; }

    public ReceivedMessage(Message message)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Status = MessageStatus.Unread;
    }

    public void MarkAsRead()
    {
        if (Status == MessageStatus.Read)
        {
            throw new InvalidOperationException("Сообщение уже отмечено как прочитанное.");
        }

        Status = MessageStatus.Read;
    }
}