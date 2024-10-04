using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class StationSegment : IRouteSegment
{
    public double Distance => 0;

    public double Force => 0;

    public int PassengersGettingOn { get; }

    public int PassengersGettingOff { get; }

    public double MaxAllowedSpeed { get; }

    public StationSegment(int passengersGettingOn, int passengersGettingOff, double maxAllowedSpeed)
    {
        PassengersGettingOn = passengersGettingOn;
        PassengersGettingOff = passengersGettingOff;
        MaxAllowedSpeed = maxAllowedSpeed;
    }

    public Result CanStop(ITrain train)
    {
        if (train.Speed <= MaxAllowedSpeed) return new Result.Success();
        return new Result.SpeedLimitExceeded(train.Speed);
    }

    public Result StopTrain(ITrain train)
    {
        Result canStopResult = CanStop(train);

        if (canStopResult is Result.Failure failure)
        {
            return failure;
        }

        train.Acceleration = 0;

        return new Result.Success();
    }

    public Result CalculateSegmentTime(ITrain train)
    {
        Result canStopResult = CanStop(train);

        if (canStopResult is Result.Failure failure)
        {
            return failure;
        }

        double stopTime = PassengersGettingOn + PassengersGettingOff;

        Result stopResult = StopTrain(train);
        if (stopResult is Result.Failure)
        {
            return stopResult;
        }

        return new Result.SuccessWithTime(stopTime);
    }
}