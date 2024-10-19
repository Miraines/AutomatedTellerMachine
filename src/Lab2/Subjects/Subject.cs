using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class Subject
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IReadOnlyCollection<Labwork> Labworks { get; set; }

    public IReadOnlyCollection<Lection> Lections { get; set; }

    public IUser Author { get; set; }

    public Guid? OriginalSubjectId { get; set; }

    public Subject(
        string name,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections,
        IUser author,
        Guid? originalSubjectId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Labworks = labworks;
        Lections = lections;
        Author = author;
        OriginalSubjectId = originalSubjectId;
    }

    public int TotalPoints()
    {
        int totalLabPoints = Labworks.Sum(lab => lab.Points);
        int totalLectionPoints = Lections.Sum(lec => lec.Points);
        return totalLabPoints + totalLectionPoints;
    }

    public bool CanEdit(IUser user)
    {
        return Author.Id.Equals(user.Id);
    }

    public Subject Clone()
    {
        return new Subject(Name, Labworks.ToList(), Lections.ToList(), Author, Id);
    }

    public void Update(
        IUser user,
        string name,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections)
    {
        if (!CanEdit(user))
        {
            throw new UnauthorizedAccessException("Only the author can edit the subject.");
        }

        Name = name;
        Labworks = labworks;
        Lections = lections;
    }
}