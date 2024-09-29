using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface ITrain
{
    double Speed { get; set; }

    double Acceleration { get; set; }

    double Weight { get; }

    double Accuracy { get; }

    double MaximumPermissibleForce { get; }

    Result TryApplyForce(double force);
}