using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

public interface IMaterials
{
    public Guid Id { get; }

    public string Title { get; }

    public string Description { get; }

    public IUser Author { get; }

    Guid? OriginalId { get; }

    IMaterials Clone();
}