using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class LabworkCreator : MaterialsCreator
{
    private readonly string _title;

    private readonly IUser _author;

    private readonly string _description;

    private readonly int _points;

    public LabworkCreator(string title, IUser author, string description, int points)
    {
        _title = title;
        _author = author;
        _description = description;
        _points = points;
    }

    public override IMaterials CreateMaterials()
    {
        return new Labwork(_title, _author, _description, _points);
    }
}