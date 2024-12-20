namespace Domain.Exceptions;

public class InvalidPinException : DomainException
{
    public InvalidPinException(string message) : base(message) { }
}