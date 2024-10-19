namespace Itmo.ObjectOrientedProgramming.Lab2.User;

public class DefaultUserCreator : UserCreator
{
    public override IUser CreateUser(string name)
    {
        return new DefaultUser(name);
    }
}