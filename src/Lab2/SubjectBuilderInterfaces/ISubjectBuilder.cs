using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;

public interface ISubjectBuilder
{
    ISubjectBuilder SetName(string name);

    ISubjectBuilder SetLabworks(IReadOnlyCollection<Labwork> labworks);

    SubjectBuilder SetLectures(IReadOnlyCollection<Lection> lectures);

    ISubjectBuilder SetAuthor(IUser author);

    Subject Build();
}