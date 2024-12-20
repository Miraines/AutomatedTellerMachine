using Domain.Exceptions;

namespace Domain.Entities;

public class Admin
{
    public Guid Id { get; }

    public string Password { get; private set; }

    public Admin(Guid id, string password)
    {
        if (id == Guid.Empty)
            throw new DomainException("Admin ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(password))
            throw new DomainException("Admin password cannot be empty.");

        Id = id;
        Password = password;
    }

    public bool VerifyPassword(string passwordToCheck)
    {
        return Password == passwordToCheck;
    }

    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new DomainException("New admin password cannot be empty.");
        Password = newPassword;
    }
}