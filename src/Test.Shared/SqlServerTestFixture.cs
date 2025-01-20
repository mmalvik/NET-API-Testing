using Testcontainers.MsSql;
using Xunit;

namespace Test.Shared;

/// <summary>
/// A shared test fixture running SQL Server in a container.
/// </summary>
public class SqlServerTestFixture : IAsyncLifetime
{
    private MsSqlContainer _mssqlContainer;

    /// <summary>
    /// Starts the SQL Server container.
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _mssqlContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();

            await _mssqlContainer.StartAsync();
            ConnectionString = _mssqlContainer.GetConnectionString();

        }
        catch (Exception exception)
        {
            throw new Exception("Do you have Docker running?", exception);
        }
    }

    /// <summary>
    /// Disposes the testcontainer.
    /// </summary>
    public async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }

    /// <summary>
    /// The connection string for the database.
    /// </summary>
    public string ConnectionString { get; private set; }
}