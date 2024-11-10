using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;

public interface IMessenger
{
    void Print(Message message);
}