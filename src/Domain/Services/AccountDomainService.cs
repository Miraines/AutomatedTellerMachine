using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Services;

public class AccountDomainService
{
    public void VerifyPin(Account account, Pin enteredPin)
    {
        if (!account.Pin.Equals(enteredPin))
            throw new InvalidPinException("Invalid PIN entered.");
    }
}