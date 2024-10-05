using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface ITrain
{
    double Speed { get; }

    double Acceleration { get; }

    double Weight { get; }

    double Accuracy { get; }

    double MaximumPermissibleForce { get; }

    Result TryApplyForce(double force);

    void TryApplyAcceleration(double acceleration);

    void TryApplySpeed(double speed);
}