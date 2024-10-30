using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;

public interface ICreditSubjectBuilder
{
    ICreditSubjectBuilder SetName(string name);

    ICreditSubjectBuilder SetLabworks(IReadOnlyCollection<Labwork> labworks);

    ICreditSubjectBuilder SetLectures(IReadOnlyCollection<Lection> lectures);

    ICreditSubjectBuilder SetAuthor(IUser author);

    ICreditSubjectBuilder SetMinPointsForCredit(int minPoints);

    CreditSubject Build();
}