using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;

public class SubjectBuilder : ISubjectBuilder
{
    private string? _name;

    private IReadOnlyCollection<Labwork>? _labworks;

    private IReadOnlyCollection<Lection>? _lectures;

    private IUser? _author;

    private Guid? _baseSubjectId;

    public ISubjectBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ISubjectBuilder SetLabworks(IReadOnlyCollection<Labwork> labworks)
    {
        _labworks = labworks;
        return this;
    }

    public SubjectBuilder SetLectures(IReadOnlyCollection<Lection> lectures)
    {
        _lectures = lectures;
        return this;
    }

    public ISubjectBuilder SetAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public ISubjectBuilder SetBaseSubjectId(Guid? baseSubjectId)
    {
        _baseSubjectId = baseSubjectId;
        return this;
    }

    public Subject Build()
    {
        if (_name == null || _labworks == null || _lectures == null || _author == null)
        {
            throw new InvalidOperationException("Name and Labwork and Lecture are required.");
        }

        return new Subject(_name, _labworks, _lectures, _author, _baseSubjectId);
    }
}