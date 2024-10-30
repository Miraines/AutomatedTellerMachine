namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public interface IRepository<T> where T : class
{
    void Add(T entity);

    T? GetById(Guid id);

    IEnumerable<T> GetAll();

    void Remove(Guid id);
}