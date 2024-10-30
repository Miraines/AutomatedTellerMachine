using Itmo.ObjectOrientedProgramming.Lab2.Lections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LectionRepository : IRepository<Lection>
{
    private readonly Dictionary<Guid, Lection> _lections = new Dictionary<Guid, Lection>();

    public void Add(Lection entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Лекция не может быть null.");

        if (_lections.ContainsKey(entity.Id))
            throw new ArgumentException($"Лекция с Id {entity.Id} уже существует.");

        _lections[entity.Id] = entity;
    }

    public Lection? GetById(Guid id)
    {
        _lections.TryGetValue(id, out Lection? lection);
        return lection;
    }

    public IEnumerable<Lection> GetAll()
    {
        return _lections.Values;
    }

    public void Remove(Guid id)
    {
        _lections.Remove(id);
    }
}