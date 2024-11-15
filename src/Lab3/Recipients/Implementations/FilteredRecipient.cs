using Itmo.ObjectOrientedProgramming.Lab3.Filters;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Implementations;

public class FilteredRecipient : IRecipient
{
    private readonly IRecipient _innerRecipient;
    private readonly IFilter _filter;

    public FilteredRecipient(IRecipient innerRecipient, IFilter filter)
    {
        _innerRecipient = innerRecipient ?? throw new ArgumentNullException(nameof(innerRecipient));
        _filter = filter ?? throw new ArgumentNullException(nameof(filter));
    }

    public void Receive(Message message)
    {
        if (_filter.ShouldProcess(message))
        {
            _innerRecipient.Receive(message);
        }
    }
}