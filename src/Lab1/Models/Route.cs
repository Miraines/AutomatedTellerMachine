using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Route
{
    private readonly List<IRouteSegment> _segments;

    public IFinalSegment FinalSegment { get; }

    public Route(IReadOnlyList<IRouteSegment> segments, IFinalSegment finalSegment)
    {
        if (segments == null || segments.Count == 0)
        {
            throw new ArgumentException("The route must contain at least one segment.");
        }

        if (finalSegment is null)
        {
            throw new ArgumentNullException(nameof(finalSegment), "Final segment must be provided.");
        }

        _segments = new List<IRouteSegment>(segments);
        FinalSegment = finalSegment;
    }

    public void Add(IRouteSegment segment)
    {
        _segments.Add(segment);
    }

    public void Delete(IRouteSegment segment)
    {
        _segments.Remove(segment);
    }

    public int Count()
    {
        return _segments.Count;
    }

    public IReadOnlyList<IRouteSegment> GetSegments()
    {
        return _segments.AsReadOnly();
    }

    public IRouteSegment GetSegment(int index)
    {
        if (index < 0 || index >= _segments.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid segment index.");
        }

        return _segments[index];
    }
}