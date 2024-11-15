using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;

public class LoggedRecipients : IRecipient
{
    private readonly IRecipient _innerRecipient;
    private readonly ILogger _logger;

    public LoggedRecipients(IRecipient innerRecipient, ILogger logger)
    {
        _innerRecipient = innerRecipient ?? throw new ArgumentNullException(nameof(innerRecipient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Receive(Message message)
    {
        _logger.Log($"Recipient received message ID: {message.Id}, Importance Level: {message.ImportanceLevel}");
        _innerRecipient.Receive(message);
    }
}