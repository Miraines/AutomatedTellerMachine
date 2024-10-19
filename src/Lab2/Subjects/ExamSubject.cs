using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ExamSubject : Subject
{
    public int MaxPoints { get; private set; }

    public ExamSubject(
        string name,
        IUser author,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections,
        int maxPoints)
        : base(name, labworks, lections, author)
    {
        MaxPoints = maxPoints > 0
            ? maxPoints
            : throw new ArgumentOutOfRangeException(nameof(maxPoints), "MaxPoints must be greater than zero.");
        ValidateTotalPoints();
    }

    public new ExamSubject Clone()
    {
        return new ExamSubject(Name, Author, Labworks, Lections, MaxPoints)
        {
            OriginalSubjectId = this.OriginalSubjectId,
        };
    }

    public void Update(
        IUser user,
        string name,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections,
        int maxPoints)
    {
        if (!CanEdit(user))
        {
            throw new UnauthorizedAccessException("Only the author can edit the subject.");
        }

        Name = name;
        Labworks = labworks;
        Lections = lections;
        MaxPoints = maxPoints > 0
            ? maxPoints
            : throw new ArgumentOutOfRangeException(nameof(maxPoints), "MaxPoints must be greater than zero.");
        ValidateTotalPoints();
    }

    private void ValidateTotalPoints()
    {
        int totalPoints = TotalPoints();
        if (totalPoints + MaxPoints != 100)
        {
            throw new InvalidOperationException("The total points for the subject must equal 100.");
        }
    }
}