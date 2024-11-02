using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ExamSubject : ISubject
{
    public Guid Id { get; }

    public string Name { get; set; }

    public IReadOnlyCollection<Labwork> Labworks { get; set; }

    public IReadOnlyCollection<Lection> Lections { get; set; }

    public IUser Author { get; }

    public Guid? OriginalSubjectId { get; }

    public int MaxPoints { get; }

    public ExamSubject(
        string name,
        IUser author,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections,
        int maxPoints,
        Guid? originalSubjectId = null)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Labworks = labworks ?? throw new ArgumentNullException(nameof(labworks));
        Lections = lections ?? throw new ArgumentNullException(nameof(lections));
        MaxPoints = maxPoints > 0
            ? maxPoints
            : throw new ArgumentOutOfRangeException(nameof(maxPoints), "MaxPoints must be greater than zero.");
        OriginalSubjectId = originalSubjectId;
        ValidateTotalPoints();
    }

    public int TotalPoints()
    {
        return Labworks.Sum(lab => lab.Points) + Lections.Sum(lec => lec.Points);
    }

    public bool CanEdit(IUser user)
    {
        return Author.Id.Equals(user.Id);
    }

    public ISubject Clone()
    {
        return new ExamSubject(Name, Author, Labworks, Lections, MaxPoints, Id);
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

        Name = name ?? throw new ArgumentNullException(nameof(name));
        Labworks = labworks ?? throw new ArgumentNullException(nameof(labworks));
        Lections = lections ?? throw new ArgumentNullException(nameof(lections));
        ValidateTotalPoints();
    }

    private void ValidateTotalPoints()
    {
        if (TotalPoints() + MaxPoints != 100)
        {
            throw new InvalidOperationException("The total points for the subject must equal 100.");
        }
    }
}