namespace Itmo.ObjectOrientedProgramming.Lab2.User;

public interface IUserFactory
{
    IUser CreateUser(string name);
}