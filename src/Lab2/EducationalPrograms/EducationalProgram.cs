using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public class EducationalProgram
{
    public Guid Id { get; }

    public string Name { get; set; }

    private readonly List<Semestr> _semesters;

    public IReadOnlyCollection<Semestr> Semesters => _semesters.AsReadOnly();

    public IUser ResponsiblePerson { get; set; }

    public IUser ProgramLeader { get; set; }

    public EducationalProgram(string name, IUser responsiblePerson, IUser programLeader)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "name cannot be null or empty");
        }

        Id = Guid.NewGuid();
        ProgramLeader = programLeader ??
                        throw new ArgumentNullException(nameof(programLeader), "program leader cannot be null");
        ResponsiblePerson = responsiblePerson ??
                            throw new ArgumentNullException(
                                nameof(responsiblePerson),
                                "responsible person cannot be null");
        Name = name;
        _semesters = new List<Semestr>();
    }

    public void AddSemester(Semestr semester)
    {
        ArgumentNullException.ThrowIfNull(semester);
        if (_semesters.Any(s => s.Number == semester.Number))
            throw new InvalidOperationException($"Семестр номер {semester.Number} уже существует.");

        _semesters.Add(semester);
    }

    public bool RemoveSemester(int semesterNumber)
    {
        Semestr? semester = _semesters.FirstOrDefault(s => s.Number == semesterNumber);
        if (semester == null)
            return false;

        return _semesters.Remove(semester);
    }

    public void AddSubjectToSemester(int semesterNumber, ISubject subject)
    {
        Semestr? semester = _semesters.FirstOrDefault(s => s.Number == semesterNumber);
        if (semester == null)
            throw new InvalidOperationException($"Семестр номер {semesterNumber} не найден.");

        semester.AddSubject(subject);
    }

    public void Validate()
    {
        foreach (Semestr semester in _semesters)
        {
            if (!semester.HasAtLeastOneSubject())
            {
                throw new InvalidOperationException(
                    $"Семестр {semester.Number} должен содержать хотя бы один предмет.");
            }
        }
    }

    public void UpdateProgramDetails(string newName, IUser newResponsiblePerson, IUser newProgramLeader)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Название программы не может быть пустым.", nameof(newName));
        ArgumentNullException.ThrowIfNull(newResponsiblePerson);
        ArgumentNullException.ThrowIfNull(newProgramLeader);

        Name = newName;
        ResponsiblePerson = newResponsiblePerson;
        ProgramLeader = newProgramLeader;
    }
}