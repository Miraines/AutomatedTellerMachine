using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;

public interface IMaterials
{
    Guid Id { get; }

    string Title { get; }

    string Description { get; }

    IUser Author { get; }

    Guid? OriginalId { get; }
}