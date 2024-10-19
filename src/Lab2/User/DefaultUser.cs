namespace Itmo.ObjectOrientedProgramming.Lab2.User;

public class DefaultUser : IUser
{
    public string Name { get; }

    public Guid Id { get; }

    public DefaultUser(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
}