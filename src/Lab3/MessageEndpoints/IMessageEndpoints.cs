using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;

public interface IMessageEndpoints
{
    void SendMessage(Message message);
}