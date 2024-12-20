using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Account
{
    public string AccountNumber { get; }

    public Pin Pin { get; private set; }

    public decimal Balance { get; private set; }

    public Account(string accountNumber, Pin pin, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new DomainException("Account number cannot be empty.");
        if (initialBalance < 0)
            throw new DomainException("Initial balance cannot be negative.");

        AccountNumber = accountNumber;
        Pin = pin;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Deposit amount must be positive.");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Withdraw amount must be positive.");

        if (amount > Balance)
            throw new InsufficientFundsException("Not enough funds to complete the withdrawal.");

        Balance -= amount;
    }

    public void ChangePin(Pin newPin)
    {
        Pin = newPin ?? throw new DomainException("New PIN cannot be null.");
    }
}