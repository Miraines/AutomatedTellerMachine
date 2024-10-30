using Itmo.ObjectOrientedProgramming.Lab2.Labworks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LabworkRepository : IRepository<Labwork>
{
    private readonly Dictionary<Guid, Labwork> _labworks = new Dictionary<Guid, Labwork>();

    public void Add(Labwork entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Лабораторная работа не может быть null.");

        if (_labworks.ContainsKey(entity.Id))
            throw new ArgumentException($"Лабораторная работа с Id {entity.Id} уже существует.");

        _labworks[entity.Id] = entity;
    }

    public Labwork? GetById(Guid id)
    {
        _labworks.TryGetValue(id, out Labwork? labwork);
        return labwork;
    }

    public IEnumerable<Labwork> GetAll()
    {
        return _labworks.Values;
    }

    public void Remove(Guid id)
    {
        _labworks.Remove(id);
    }
}