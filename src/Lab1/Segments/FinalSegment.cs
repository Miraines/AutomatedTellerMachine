using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class FinalSegment : IFinalSegment
{
    public double MaxAllowedSpeed { get; }

    public FinalSegment(double maxAllowedSpeed)
    {
        MaxAllowedSpeed = maxAllowedSpeed;
    }

    public bool CheckSpeed(ITrain train)
    {
        return train.Speed <= MaxAllowedSpeed;
    }
}