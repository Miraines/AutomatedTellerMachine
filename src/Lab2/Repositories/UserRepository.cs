using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class UserRepository : IRepository<IUser>
{
    private readonly Dictionary<Guid, IUser> _users = new Dictionary<Guid, IUser>();

    public void Add(IUser entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "Пользователь не может быть null");
        }

        if (_users.ContainsKey(entity.Id))
        {
            throw new ArgumentException("Пользователь уже существует");
        }

        _users[entity.Id] = entity;
    }

    public IUser? GetById(Guid id)
    {
        _users.TryGetValue(id, out IUser? user);
        return user;
    }

    public IEnumerable<IUser> GetAll()
    {
        return _users.Values;
    }

    public void Remove(Guid id)
    {
        _users.Remove(id);
    }
}