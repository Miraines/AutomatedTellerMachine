using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lections;

public class Lection : IMaterials, IPrototype<Lection>
{
    public string Title { get; private set; }

    public IUser Author { get; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    public Guid Id { get; }

    public int Points { get; private set; }

    public Guid? OriginalId { get; }

    public Lection(
        string title,
        IUser author,
        string description,
        string content,
        int points,
        Guid? originalId = default)
    {
        Title = title;
        Description = description;
        Author = author;
        Id = Guid.NewGuid();
        Points = points;
        Content = content;
        OriginalId = originalId;
    }

    public Lection Clone()
    {
        return new Lection(Title, Author, Description, Content, Points, Id);
    }

    public void Update(string newTitle, string newDescription, string newContent, int points, IUser newAuthor)
    {
        if (newAuthor.Id != Author.Id)
        {
            throw new UnauthorizedAccessException("Только автор может изменять эту лекцию.");
        }

        Title = newTitle;

        Description = newDescription;

        Content = newContent;

        Points = points;
    }
}