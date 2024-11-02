using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

public interface IMaterialsFactory
{
    Lection CreateLection(string title, IUser author, string description, string content, int points);

    Labwork CreateLabwork(string title, IUser author, string description, int points);
}