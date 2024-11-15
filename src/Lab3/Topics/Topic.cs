using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public class Topic
{
    public string TopicName { get; }

    private readonly List<IRecipient> _recipients;

    public IReadOnlyList<IRecipient> Recipients => _recipients.AsReadOnly();

    public Topic(string topicName)
    {
        if (string.IsNullOrWhiteSpace(topicName))
        {
            throw new ArgumentException($"'{nameof(topicName)}' cannot be null or whitespace.", nameof(topicName));
        }

        TopicName = topicName;
        _recipients = new List<IRecipient>();
    }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void Send(Message message)
    {
        foreach (IRecipient recipient in Recipients)
        {
            recipient.Receive(message);
        }
    }
}