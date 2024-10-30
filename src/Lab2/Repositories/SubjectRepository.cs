using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class SubjectRepository : IRepository<ISubject>
{
    private readonly Dictionary<Guid, ISubject> _subjects = new Dictionary<Guid, ISubject>();

    public void Add(ISubject entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Предмет не может быть null.");

        if (_subjects.ContainsKey(entity.Id))
            throw new ArgumentException($"Предмет с Id {entity.Id} уже существует.");

        _subjects[entity.Id] = entity;
    }

    public ISubject? GetById(Guid id)
    {
        _subjects.TryGetValue(id, out ISubject? subject);
        return subject;
    }

    public IEnumerable<ISubject> GetAll()
    {
        return _subjects.Values;
    }

    public void Remove(Guid id)
    {
        _subjects.Remove(id);
    }
}