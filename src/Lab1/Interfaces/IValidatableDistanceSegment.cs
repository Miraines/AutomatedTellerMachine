namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IValidatableDistanceSegment : IRouteSegment
{
    void EnsureValidDistance(double distance);
}