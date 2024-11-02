using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;

public interface IExamSubjectBuilder
{
    IExamSubjectBuilder SetName(string name);

    IExamSubjectBuilder SetLabworks(IReadOnlyCollection<Labwork> labworks);

    IExamSubjectBuilder SetLectures(IReadOnlyCollection<Lection> lectures);

    IExamSubjectBuilder SetAuthor(IUser author);

    IExamSubjectBuilder SetMaxPoints(int maxPoints);

    ExamSubject Build();
}