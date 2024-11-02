using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

public class MaterialsFactory : IMaterialsFactory
{
    public Lection CreateLection(string title, IUser author, string description, string content, int points)
    {
        return new Lection(title, author, description, content, points);
    }

    public Labwork CreateLabwork(string title, IUser author, string description, int points)
    {
        return new Labwork(title, author, description, points);
    }
}