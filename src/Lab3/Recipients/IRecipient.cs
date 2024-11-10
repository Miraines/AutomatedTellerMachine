using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public interface IRecipient
{
    void Receive(Message message);

    int MinimumImportanceLevel { get; }
}