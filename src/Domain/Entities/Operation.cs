using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Operation
{
    public Guid Id { get; }

    public string AccountNumber { get; }

    public OperationType Type { get; }

    public decimal Amount { get; }

    public DateTime Timestamp { get; }

    public Operation(string accountNumber, OperationType type, decimal amount, DateTime timestamp)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new DomainException("Account number cannot be empty.");

        if (amount < 0)
            throw new DomainException("Operation amount cannot be negative.");

        Id = Guid.NewGuid();
        AccountNumber = accountNumber;
        Type = type;
        Amount = amount;
        Timestamp = timestamp;
    }

    // Дополнительный конструктор для восстановления операции из БД с уже известным Id
    public Operation(Guid id, string accountNumber, OperationType type, decimal amount, DateTime timestamp)
    {
        if (id == Guid.Empty)
            throw new DomainException("Operation ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new DomainException("Account number cannot be empty.");

        if (amount < 0)
            throw new DomainException("Operation amount cannot be negative.");

        Id = id;
        AccountNumber = accountNumber;
        Type = type;
        Amount = amount;
        Timestamp = timestamp;
    }
}