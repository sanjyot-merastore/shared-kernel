using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MeraStore.Shared.Kernel.Persistence;

/// <summary>
/// Generic base class for design-time DbContext factory creation.
/// Used to support EF Core CLI tools for migrations without relying on dependency injection.
/// </summary>
/// <typeparam name="TContext">The type of the DbContext to create.</typeparam>
public abstract class BaseDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// The name of the connection string to load from configuration.
    /// </summary>
    protected abstract string ConnectionStringName { get; }

    /// <summary>
    /// Creates a new instance of the DbContext using design-time configuration.
    /// </summary>
    /// <param name="args">Optional arguments passed by the CLI.</param>
    /// <returns>An instance of the specified DbContext.</returns>
    public TContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        Configure(optionsBuilder, configuration);

        return CreateNewInstance(optionsBuilder.Options);
    }

    /// <summary>
    /// Allows subclasses to configure the DbContext options with the desired provider.
    /// </summary>
    /// <param name="optionsBuilder">The DbContext options builder.</param>
    /// <param name="configuration">The app configuration root.</param>
    protected virtual void Configure(DbContextOptionsBuilder<TContext> optionsBuilder, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        // Default to SQL Server — override if needed
        optionsBuilder.UseSqlServer(connectionString);
    }

    /// <summary>
    /// Subclasses must implement this to return a properly constructed DbContext instance.
    /// </summary>
    /// <param name="options">The configured DbContext options.</param>
    /// <returns>A new instance of the DbContext.</returns>
    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
}
