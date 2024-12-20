using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class AdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
    }

    public void ValidateAdminPassword(string password)
    {
        Admin? admin = _adminRepository.GetAdmin();
        if (admin == null || !admin.VerifyPassword(password))
        {
            throw new ValidationException("Invalid admin password.");
        }
    }

    public void ChangeAdminPassword(string oldPassword, string newPassword)
    {
        Admin? admin = _adminRepository.GetAdmin();
        if (admin == null)
            throw new ValidationException("Admin not found.");

        if (!admin.VerifyPassword(oldPassword))
            throw new ValidationException("Old password is incorrect.");

        admin.ChangePassword(newPassword);
        _adminRepository.UpdateAdmin(admin);
    }

    public Admin? GetAdmin()
    {
        return _adminRepository.GetAdmin();
    }

    public void EnsureDefaultAdminExists(string defaultPassword)
    {
        Admin? admin = _adminRepository.GetAdmin();
        if (admin == null)
        {
            var newAdmin = new Admin(Guid.NewGuid(), defaultPassword);
            _adminRepository.CreateAdmin(newAdmin);
        }
    }
}