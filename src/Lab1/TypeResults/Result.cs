namespace Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

public abstract record Result
{
    private Result() { }

    public sealed record Success : Result;

    public sealed record SuccessWithTime(double Time) : Result;

    public sealed record Failure(string ErrorMessage) : Result;
}