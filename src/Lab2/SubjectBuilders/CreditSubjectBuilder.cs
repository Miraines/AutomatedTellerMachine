using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;

public class CreditSubjectBuilder : ICreditSubjectBuilder
{
    private string? _name;

    private IReadOnlyCollection<Labwork>? _labworks;

    private IReadOnlyCollection<Lection>? _lectures;

    private IUser? _author;

    private int _minPoints;

    public ICreditSubjectBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ICreditSubjectBuilder SetLabworks(IReadOnlyCollection<Labwork> labworks)
    {
        _labworks = labworks;
        return this;
    }

    public ICreditSubjectBuilder SetLectures(IReadOnlyCollection<Lection> lectures)
    {
        _lectures = lectures;
        return this;
    }

    public ICreditSubjectBuilder SetAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public ICreditSubjectBuilder SetMinPointsForCredit(int minPoints)
    {
        _minPoints = minPoints;
        return this;
    }

    public CreditSubject Build()
    {
        if (_name == null || _labworks == null || _lectures == null || _author == null || _minPoints <= 0)
        {
            throw new InvalidOperationException("Name and Labwork and Lecture are required.");
        }

        return new CreditSubject(_name, _author, _labworks, _lectures, _minPoints);
    }
}