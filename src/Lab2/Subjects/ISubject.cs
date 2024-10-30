using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface ISubject : IPrototype<ISubject>
{
    Guid Id { get; }

    string Name { get; set; }

    IReadOnlyCollection<Labwork> Labworks { get; set; }

    IReadOnlyCollection<Lection> Lections { get; set; }

    IUser Author { get; }

    Guid? OriginalSubjectId { get; }

    int TotalPoints();

    bool CanEdit(IUser user);

    void Update(
        IUser user,
        string name,
        IReadOnlyCollection<Labwork> labworks,
        IReadOnlyCollection<Lection> lections);
}