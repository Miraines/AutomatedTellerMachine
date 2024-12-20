using Application.Interfaces;
using Application.Services;
using Domain.Services;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Migrations;
using Infrastructure.Repositories;

namespace UI;

internal abstract class Program
{
    private static void Main()
    {
        var dbConfig = new DatabaseConfig("Host=localhost;Port=5432;Database=atmdb;Username=maust;Password=3936570359az;");
        MigrationRunner.RunMigrations(dbConfig.ConnectionString);

        var connectionFactory = new DbConnectionFactory(dbConfig);

        IAccountRepository accountRepository = new PostgresAccountRepository(connectionFactory);
        IAdminRepository adminRepository = new PostgresAdminRepository(connectionFactory);
        IOperationRepository operationRepository = new PostgresOperationRepository(connectionFactory);

        var accountDomainService = new AccountDomainService();
        var accountService = new AccountService(accountRepository, operationRepository, accountDomainService);
        var adminService = new AdminService(adminRepository);

        adminService.EnsureDefaultAdminExists("admin123");

        var ui = new ConsoleUI(accountService, adminService);
        ui.Run();
    }
}