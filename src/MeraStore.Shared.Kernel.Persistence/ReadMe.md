# MeraStore.Shared.Kernel.Persistence

A reusable EF Core persistence library providing base abstractions and implementations for repository, unit of work, auditing, and soft-deletion strategies.

---

## ✨ Features

- Generic `IRepository` and `IReadOnlyRepository` interfaces
- `UnitOfWork` abstraction for transactional consistency
- `DbContextBase` with:
  - Automatic audit tracking (`CreatedDate`, `ModifiedDate`)
  - Soft-delete support (`DeletedDate`, `IsDeleted`)
  - Global query filters for soft-deleted entities
- Strategy pattern for save changes customization
- Clean separation of concerns and SOLID design

---

## 📦 Installation

This is part of the MeraStore.Shared.Kernel ecosystem. Add a reference to this project via:

```bash
dotnet add reference ../MeraStore.Shared.Kernel.Persistence/MeraStore.Shared.Kernel.Persistence.csproj
```


### 🧪 Example Usage
#### Define your entity
```
public class Product : IAuditable, ISoftDeletable
{
    public Ulid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted => DeletedDate != null;
}
```
#### Create a DbContext
```
public class ProductDbContext : DbContextBase
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}
```

## 🔧 Service Registration
To register the EF Core persistence services in your application, use one of the provided extension methods from MeraStore.Shared.Kernel.Persistence.EFCore.

### Option 1: Basic Setup (Without Unit of Work)
```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```

✅ This registers AppDbContext, IRepository<T>, and IReadOnlyRepository<T> with a NoOpSaveChangesStrategy (no Unit of Work behavior).

### Option 2: With Custom Unit of Work

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceWithUnitOfWork<AppDbContext, IAppUnitOfWork, AppUnitOfWork>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
✅ This registers AppDbContext, repositories, and binds your IAppUnitOfWork interface to its implementation AppUnitOfWork.