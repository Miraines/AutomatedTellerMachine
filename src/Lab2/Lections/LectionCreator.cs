using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lections;

public class LectionCreator : MaterialsCreator
{
    private readonly string _title;

    private readonly IUser _author;

    private readonly string _description;

    private readonly string _content;

    private readonly int _points;

    public LectionCreator(string title, IUser author, string description, string content, int points)
    {
        _title = title;
        _author = author;
        _description = description;
        _points = points;
        _content = content;
    }

    public override IMaterials CreateMaterials()
    {
        return new Lection(_title, _author, _description, _content, _points);
    }
}