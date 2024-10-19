using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class CreditSubject : Subject
{
    public int MinPointsForCredit { get; set; }

    public CreditSubject(
        string name,
        IUser author,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections,
        int minPointsForCredit)
        : base(name, labworks, lections, author)
    {
        MinPointsForCredit = minPointsForCredit;
        ValidateTotalPoints();
    }

    public new CreditSubject Clone()
    {
        return new CreditSubject(Name, Author, Labworks, Lections, MinPointsForCredit)
        {
            OriginalSubjectId = this.OriginalSubjectId,
        };
    }

    public void Update(
        IUser user,
        string name,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections,
        int minPointsForCredit)
    {
        if (!CanEdit(user))
        {
            throw new UnauthorizedAccessException("Only the author can edit the subject.");
        }

        Name = name;
        Labworks = labworks;
        Lections = lections;
        MinPointsForCredit = minPointsForCredit; // Установить новый минимум для кредита
        ValidateTotalPoints(); // Проверка валидности после обновления
    }

    private void ValidateTotalPoints()
    {
        int totalPoints = TotalPoints();
        if (totalPoints != 100)
        {
            throw new InvalidOperationException("The total points for the subject must equal 100.");
        }
    }
}