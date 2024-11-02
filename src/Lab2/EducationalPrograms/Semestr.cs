using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public class Semestr
{
    public int Number { get; }

    private readonly List<ISubject> _subjects;

    public IReadOnlyCollection<ISubject> Subjects => _subjects.AsReadOnly();

    public Semestr(int number, IReadOnlyCollection<ISubject> subjects)
    {
        if (number <= 0)
        {
            throw new ArgumentException("Number must be greater than 0", nameof(number));
        }

        if (subjects is null)
        {
            throw new ArgumentNullException(nameof(subjects), "Subjects cannot be null.");
        }

        if (subjects.Count == 0)
        {
            throw new ArgumentException("Список предметов не может быть пустым.", nameof(subjects));
        }

        Number = number;
        _subjects = new List<ISubject>(subjects);
    }

    public bool HasAtLeastOneSubject()
    {
        return _subjects.Count != 0;
    }

    public void AddSubject(ISubject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);
        _subjects.Add(subject);
    }

    public void RemoveSubject(ISubject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);
        _subjects.Remove(subject);
    }
}