using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;

public class GroupRecipient : IRecipient
{
    private readonly List<IRecipient> _subRecipients;

    public IReadOnlyList<IRecipient> SubRecipients => _subRecipients.AsReadOnly();

    public GroupRecipient()
    {
        _subRecipients = new List<IRecipient>();
    }

    public void AddSubRecipient(IRecipient recipient)
    {
        _subRecipients.Add(recipient ?? throw new ArgumentNullException(nameof(recipient)));
    }

    public void Receive(Message message)
    {
        foreach (IRecipient recipient in SubRecipients)
        {
            recipient.Receive(message);
        }
    }
}