using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class Labwork : IMaterials
{
    public string Title { get; private set; }

    public IUser Author { get; }

    public string Description { get; private set; }

    public Guid Id { get; }

    public int Points { get; private set; }

    public Guid? OriginalId { get; private set; }

    public Labwork(string title, IUser author, string description, int points, Guid originalId = default)
    {
        Title = title;
        Description = description;
        Author = author;
        Id = Guid.NewGuid();
        Points = points;
        OriginalId = originalId;
    }

    public IMaterials Clone()
    {
        return new Labwork(Title, Author, Description, Points, Id);
    }

    public void Update(string newTitle, string newDescription, int points, IUser currentUser)
    {
        if (currentUser.Id != Author.Id)
        {
            throw new UnauthorizedAccessException("Только автор может изменять лабораторную работу.");
        }

        Title = newTitle;

        Description = newDescription;

        Points = points;
    }
}