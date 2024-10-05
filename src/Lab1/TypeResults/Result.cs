namespace Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

public abstract record Result
{
    protected Result() { }

    public sealed record Success : Result;

    public sealed record SuccessWithTime(double Time) : Result;

    public abstract record Failure : Result
    {
        public abstract string ErrorMessage { get; }
    }

    public sealed record SpeedLimitExceeded(double ExceededSpeed) : Failure
    {
        public override string ErrorMessage => $"Speed limit exceeded by {ExceededSpeed}.";
    }

    public sealed record InvalidAcceleration(double Acceleration) : Failure
    {
        public override string ErrorMessage => $"Invalid acceleration: {Acceleration}.";
    }

    public sealed record ForceExceeded(double AppliedForce, double MaximumPermissibleForce) : Failure
    {
        public override string ErrorMessage => $"Applied force {AppliedForce} exceeds maximum permissible force {MaximumPermissibleForce}.";
    }

    public sealed record InvalidRoute(string Reason) : Failure
    {
        public override string ErrorMessage => $"Invalid route: {Reason}.";
    }

    public sealed record InvalidSegment(string SegmentName, string Reason) : Failure
    {
        public override string ErrorMessage => $"Invalid segment {SegmentName}: {Reason}.";
    }
}