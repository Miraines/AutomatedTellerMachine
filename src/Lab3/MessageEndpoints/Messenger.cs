using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEndpoints;

public class Messenger : IMessenger
{
    public void Print(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Console.WriteLine("Мессенджер:");
        Console.WriteLine($"ID: {message.Id}");
        Console.WriteLine($"Description: {message.Description}");
        Console.WriteLine($"Content: {message.Content}");
        Console.WriteLine($"Importance Level: {message.ImportanceLevel}");
    }
}