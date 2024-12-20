using Domain.Entities;
using Domain.Exceptions;
using Xunit;

namespace DomainTests;

public class AdminTests
{
    [Fact]
    public void Admin_CreateWithValidData_CreatedSuccessfully()
    {
        var admin = new Admin(Guid.NewGuid(), "admin123");
        Assert.NotNull(admin);
        Assert.Equal("admin123", admin.Password);
    }

    [Fact]
    public void Admin_CreateWithEmptyPassword_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new Admin(Guid.NewGuid(), string.Empty));
    }

    [Fact]
    public void Admin_CreateWithEmptyGuid_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new Admin(Guid.Empty, "password"));
    }

    [Fact]
    public void Admin_VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        var admin = new Admin(Guid.NewGuid(), "secret");
        Assert.True(admin.VerifyPassword("secret"));
    }

    [Fact]
    public void Admin_VerifyPassword_WrongPassword_ReturnsFalse()
    {
        var admin = new Admin(Guid.NewGuid(), "secret");
        Assert.False(admin.VerifyPassword("wrong"));
    }

    [Fact]
    public void Admin_ChangePassword_ToValidPassword_UpdatesPassword()
    {
        var admin = new Admin(Guid.NewGuid(), "oldpass");
        admin.ChangePassword("newpass");
        Assert.Equal("newpass", admin.Password);
    }

    [Fact]
    public void Admin_ChangePassword_ToEmpty_ThrowsDomainException()
    {
        var admin = new Admin(Guid.NewGuid(), "oldpass");
        Assert.Throws<DomainException>(() => admin.ChangePassword(string.Empty));
    }
}