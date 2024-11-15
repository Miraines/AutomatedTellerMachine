using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Filters;

public class ImportanceLevelFilter : IFilter
{
    private readonly int _minimumImportanceLevel;

    public ImportanceLevelFilter(int minimumImportanceLevel)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minimumImportanceLevel);

        _minimumImportanceLevel = minimumImportanceLevel;
    }

    public bool ShouldProcess(Message message)
    {
        return message.ImportanceLevel >= _minimumImportanceLevel;
    }
}