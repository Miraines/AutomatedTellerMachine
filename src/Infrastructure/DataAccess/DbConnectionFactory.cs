using Npgsql;
using System.Data;

namespace Infrastructure.DataAccess;

public class DbConnectionFactory
{
    private readonly DatabaseConfig _config;

    public DbConnectionFactory(DatabaseConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_config.ConnectionString);
        connection.Open();
        return connection;
    }
}