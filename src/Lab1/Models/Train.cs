using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Train : ITrain
{
    public double Weight { get; }

    public double MaximumPermissibleForce { get; }

    public double Accuracy { get; }

    public double Speed { get; set; }

    public double Acceleration { get; set; }

    public Train(double weight, double maximumPermissibleForce, double accuracy)
    {
        if (weight <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than zero.");
        }

        if (accuracy <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(accuracy), "Accuracy must be greater than zero.");
        }

        if (!IsMaximumPermissibleForceValid(maximumPermissibleForce))
        {
            throw new ArgumentOutOfRangeException(nameof(maximumPermissibleForce), "Maximum permissible force must be greater than zero.");
        }

        Weight = weight;
        MaximumPermissibleForce = maximumPermissibleForce;
        Accuracy = accuracy;
        Acceleration = 0;
        Speed = 0;
    }

    public Result TryApplyForce(double force)
    {
        if (!IsForceValid(force))
        {
            return new Result.ForceExceeded(force, MaximumPermissibleForce);
        }

        CalculateAcceleration(force);
        UpdateSpeed();

        return new Result.Success();
    }

    private Result EnsureValidSpeed()
    {
        if (Speed < 0)
        {
            return new Result.InvalidAcceleration(Speed);
        }

        return new Result.Success();
    }

    private void UpdateSpeed()
    {
        Speed += Acceleration * Accuracy;
        EnsureValidSpeed();
    }

    private void CalculateAcceleration(double force)
    {
        if (Weight == 0)
            throw new DivideByZeroException("Weight cannot be zero.");

        Acceleration = force / Weight;
    }

    private bool IsForceValid(double force)
    {
        return force <= MaximumPermissibleForce;
    }

    private bool IsMaximumPermissibleForceValid(double force)
    {
        return force > 0;
    }
}