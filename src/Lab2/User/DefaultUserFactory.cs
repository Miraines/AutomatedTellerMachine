namespace Itmo.ObjectOrientedProgramming.Lab2.User;

public class DefaultUserFactory : IUserFactory
{
    public IUser CreateUser(string name)
    {
        return new DefaultUser(name);
    }
}