using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class PowerSegment : IRouteSegment, IValidatableDistanceSegment
{
    public double Distance { get; }

    public double Force { get; }

    public PowerSegment(double distance, double force)
    {
        EnsureValidDistance(distance);
        Distance = distance;
        Force = force;
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
        Result applyForceResult = train.TryApplyForce(Force);

        if (applyForceResult is Result.Failure failure)
        {
            return new Result.Failure("Failed to apply force: " + failure.ErrorMessage);
        }

        while (distanceRemaining > 0)
        {
            double distanceTraveled = train.Speed * train.Accuracy;

            if (distanceTraveled <= 0)
            {
                return new Result.Failure("The train is unable to move further due to insufficient speed.");
            }

            distanceRemaining -= distanceTraveled;

            train.Speed += train.Acceleration * train.Accuracy;

            time += train.Accuracy;
        }

        return new Result.SuccessWithTime(time);
    }
}