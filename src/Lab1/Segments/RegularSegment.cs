using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class RegularSegment : IRouteSegment, IValidatableDistanceSegment
{
    public double Distance { get; }

    public double Force => 0;

    public RegularSegment(double distance)
    {
        EnsureValidDistance(distance);
        Distance = distance;
    }

    public void EnsureValidDistance(double distance)
    {
        if (distance < 0)
            throw new ArgumentOutOfRangeException(nameof(distance), "Distance cannot be negative.");
    }

    public Result CalculateSegmentTime(ITrain train)
    {
        double time = 0;
        double distanceRemaining = Distance;

        while (distanceRemaining > 0)
        {
            double currentSpeed = train.Speed;
            double currentAcceleration = train.Acceleration;
            double currentAccuracy = train.Accuracy;

            double speedAfterStep = currentSpeed + (currentAcceleration * currentAccuracy);
            double distanceTraveled = speedAfterStep * currentAccuracy;

            if (distanceTraveled <= 0)
            {
                return new Result.InvalidAcceleration(currentAcceleration);
            }

            distanceRemaining -= distanceTraveled;
            time += currentAccuracy;
        }

        return new Result.SuccessWithTime(time);
    }
}