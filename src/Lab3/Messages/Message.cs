namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class Message
{
    public string Description { get; }

    public string Content { get; }

    public int ImportanceLevel { get; }

    public Guid Id { get; }

    public Message(string description, string content, int importanceLevel)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentNullException(nameof(content), "Content cannot be null or empty.");
        }

        if (importanceLevel < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(importanceLevel), "Importance level cannot be negative.");
        }

        Description = description;
        Content = content;
        ImportanceLevel = importanceLevel;
        Id = Guid.NewGuid();
    }
}