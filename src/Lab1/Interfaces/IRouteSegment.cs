using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IRouteSegment
{
    double Distance { get; }

    double Force { get; }

    Result CalculateSegmentTime(ITrain train);
}