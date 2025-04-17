using MeraStore.Shared.Kernel.Persistence.Interfaces;
using MeraStore.Shared.Kernel.Persistence.Repositories;
using MeraStore.Shared.Kernel.Persistence.Strategy;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Kernel.Persistence.EFCore;

/// <summary>
/// Provides extension methods to register EF Core-based persistence services in the DI container.
/// </summary>
public static class ServiceRegistration
{
  /// <summary>
  /// Registers EF Core DbContext and generic repositories without using a Unit of Work.
  /// </summary>
  /// <typeparam name="TDbContext">The DbContext type to register.</typeparam>
  /// <param name="services">The service collection to which services will be added.</param>
  /// <param name="optionsAction">The action to configure DbContext options.</param>
  /// <returns>The updated service collection.</returns>
  public static IServiceCollection AddPersistence<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
      where TDbContext : DbContext
  {
    services.AddDbContext<TDbContext>(optionsAction);

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));

    services.AddScoped<ICommitStrategy, NoOpCommitStrategy>();

    return services;
  }

  /// <summary>
  /// Registers EF Core DbContext, generic repositories, and a custom Unit of Work implementation.
  /// </summary>
  /// <typeparam name="TDbContext">The DbContext type to register.</typeparam>
  /// <typeparam name="TIUnitOfWork">The custom Unit of Work interface.</typeparam>
  /// <typeparam name="TUnitOfWork">The concrete implementation of the Unit of Work.</typeparam>
  /// <param name="services">The service collection to which services will be added.</param>
  /// <param name="optionsAction">The action to configure DbContext options.</param>
  /// <returns>The updated service collection.</returns>
  public static IServiceCollection AddPersistenceWithUnitOfWork<TDbContext, TIUnitOfWork, TUnitOfWork>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
      where TDbContext : DbContext
      where TIUnitOfWork : class, IUnitOfWork
      where TUnitOfWork : class, TIUnitOfWork
  {
    services.AddDbContext<TDbContext>(optionsAction);

    services.AddScoped<TIUnitOfWork, TUnitOfWork>();
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));

    services.AddScoped<ICommitStrategy, NoOpCommitStrategy>();

    return services;
  }
}
