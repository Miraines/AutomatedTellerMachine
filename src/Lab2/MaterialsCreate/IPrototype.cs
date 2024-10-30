namespace Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

public interface IPrototype<T> where T : IPrototype<T>
{
    T Clone();
}