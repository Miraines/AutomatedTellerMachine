using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Process;
using Itmo.ObjectOrientedProgramming.Lab1.Segments;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;
using Xunit;

namespace Lab1.Tests;

public class RouteTests
{
    [Fact]
    public void Scenario1_ShouldReturnSuccess()
    {
        var segments = new List<IRouteSegment>
        {
            new PowerSegment(100, 50),
            new RegularSegment(200),
        };

        var finalSegment = new FinalSegment(maxAllowedSpeed: 100);

        var route = new Route(segments, finalSegment);

        var train = new Train(1000, 500, 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.SuccessWithTime>(result);

        var successResult = result as Result.SuccessWithTime;
        Assert.NotNull(successResult);
        Assert.True(successResult.Time > 0, "The route should have a valid time greater than zero.");
    }

    [Fact]
    public void Scenario2_ShouldReturnFailure()
    {
        var segments = new List<IRouteSegment>
        {
            new PowerSegment(100, 15000),
        };

        var finalSegment = new FinalSegment(1);

        var route = new Route(segments, finalSegment);

        var train = new Train(1000, 500000, 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.SpeedLimitExceeded>(result);

        var speedLimitExceededResult = result as Result.SpeedLimitExceeded;
        Assert.NotNull(speedLimitExceededResult);
    }

    [Fact]
    public void Scenario3_ShouldReturnSuccess()
    {
        var segments = new List<IRouteSegment>
        {
            new PowerSegment(100, 150),
            new RegularSegment(100),
            new StationSegment(10, 15, 100),
            new RegularSegment(100),
        };

        var finalSegment = new FinalSegment(100);

        var train = new Train(1000, 500, 0.1);

        var route = new Route(segments, finalSegment);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.SuccessWithTime>(result);

        var successResult = result as Result.SuccessWithTime;

        Assert.NotNull(successResult);

        Assert.True(successResult.Time > 0, "Общее время должно быть больше нуля.");
    }

    [Fact]
    public void Scenario4_ShouldReturnFailure()
    {
        var segments = new List<IRouteSegment>
        {
            new PowerSegment(100, 150000),
            new StationSegment(passengersGettingOn: 10, passengersGettingOff: 5, maxAllowedSpeed: 5),
            new RegularSegment(200),
        };

        var finalSegment = new FinalSegment(maxAllowedSpeed: 100000);

        var route = new Route(segments, finalSegment);

        var train = new Train(weight: 1000, maximumPermissibleForce: 160000, accuracy: 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.SpeedLimitExceeded>(result);

        var speedLimitExceededResult = result as Result.SpeedLimitExceeded;
        Assert.NotNull(speedLimitExceededResult);
    }

    [Fact]
    public void Scenario5_ShouldReturnFailure()
    {
        var segments = new List<IRouteSegment>
        {
            new PowerSegment(100, 15000),
            new RegularSegment(200),
            new StationSegment(passengersGettingOn: 10, passengersGettingOff: 5, maxAllowedSpeed: 50000),
            new RegularSegment(200),
        };

        var finalSegment = new FinalSegment(maxAllowedSpeed: 1);

        var route = new Route(segments, finalSegment);

        var train = new Train(weight: 1000, maximumPermissibleForce: 16000, accuracy: 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.SpeedLimitExceeded>(result);

        var speedLimitExceededResult = result as Result.SpeedLimitExceeded;
        Assert.NotNull(speedLimitExceededResult);
    }

    [Fact]
    public void Scenario6_ShouldReturnSuccess()
    {
        var segments = new List<IRouteSegment>
        {
            new PowerSegment(100, 15000),
            new RegularSegment(200),
            new PowerSegment(100, -14000),
            new StationSegment(15, 30, 200),
            new RegularSegment(200),
            new PowerSegment(100, 15000),
            new RegularSegment(200),
            new PowerSegment(100, -14500),
        };

        var finalSegment = new FinalSegment(maxAllowedSpeed: 100);

        var route = new Route(segments, finalSegment);

        var train = new Train(weight: 1000, maximumPermissibleForce: 40000, accuracy: 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.SuccessWithTime>(result);

        var successResult = result as Result.SuccessWithTime;
        Assert.NotNull(successResult);
        Assert.True(successResult.Time > 0, "Общее время должно быть больше нуля.");
    }

    [Fact]
    public void Scenario7_ShouldReturnFailure()
    {
        var segments = new List<IRouteSegment>
        {
            new RegularSegment(100),
        };

        var finalSegment = new FinalSegment(100);

        var route = new Route(segments, finalSegment);

        var train = new Train(1000, 500, 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.InvalidAcceleration>(result);
        var invalidAccelerationResult = result as Result.InvalidAcceleration;
        Assert.NotNull(invalidAccelerationResult);
    }

    [Fact]
    public void Scenario8_ShouldReturnFailure()
    {
        double x = 100;
        double y = 50;

        var segments = new List<IRouteSegment>
        {
            new PowerSegment(x, y),
            new PowerSegment(x, -2 * y),
        };

        var finalSegment = new FinalSegment(maxAllowedSpeed: 10000);

        var route = new Route(segments, finalSegment);

        var train = new Train(weight: 1000, maximumPermissibleForce: 5000, accuracy: 0.1);

        var process = new ProcessTrain(train, route);

        Result result = process.Process();

        Assert.IsType<Result.InvalidAcceleration>(result);

        var invalidAccelerationResult = result as Result.InvalidAcceleration;
        Assert.NotNull(invalidAccelerationResult);
    }
}