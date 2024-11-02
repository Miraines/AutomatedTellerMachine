using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;

public class ExamSubjectBuilder : IExamSubjectBuilder
{
    private string? _name;

    private IReadOnlyCollection<Labwork>? _labworks;

    private IReadOnlyCollection<Lection>? _lectures;

    private IUser? _author;

    private int _maxPoints;

    public IExamSubjectBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public IExamSubjectBuilder SetLabworks(IReadOnlyCollection<Labwork> labworks)
    {
        _labworks = labworks;
        return this;
    }

    public IExamSubjectBuilder SetLectures(IReadOnlyCollection<Lection> lectures)
    {
        _lectures = lectures;
        return this;
    }

    public IExamSubjectBuilder SetAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public IExamSubjectBuilder SetMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
        return this;
    }

    public ExamSubject Build()
    {
        if (_name == null || _labworks == null || _lectures == null || _author == null || _maxPoints <= 0)
        {
            throw new InvalidOperationException("Name and Labwork and Lecture are required.");
        }

        return new ExamSubject(_name, _author, _labworks, _lectures, _maxPoints);
    }
}