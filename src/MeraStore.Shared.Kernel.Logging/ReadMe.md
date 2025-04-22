# MeraStore.Shared.Kernel.Caching

A lightweight, flexible caching library for the **MeraStore** platform that supports both **In-Memory** and **Redis** caching using a unified `ICacheProvider` interface. Built for modularity and easy integration into microservices.

---

## ✨ Features

- **Unified `ICacheProvider` abstraction**
- **InMemoryCacheProvider** powered by `Microsoft.Extensions.Caching.Memory`
- **RedisCacheProvider** powered by `StackExchange.Redis`
- Configurable expiration settings via `CacheEntryOptions`
- Easily swap or test providers by overriding DI registrations

---

## 🧰 Registration

Add the following to your `Program.cs` or Startup file:

### ➕ In-Memory Only

```csharp
builder.Services.AddMeraStoreCaching();
````

### ➕ In-Memory + Redis

```csharp
builder.Services.AddMeraStoreCaching("localhost:6379");
```

### 🧰 Service Registration Example (With Options)
```
builder.Services.AddMeraStoreCaching(
    redisConnectionString: "localhost:6379",
    configureDefaults: options =>
    {
        options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = TimeSpan.FromMinutes(10);
        options.Priority = CacheItemPriority.High;
        options.Metadata = new Dictionary<string, string>
        {
            { "service", "product-service" },
            { "env", "development" }
        };
    });
```

### By default:

- InMemoryCacheProvider is always registered.
- If Redis connection string is provided, RedisCacheProvider is also registered.
- ICacheProvider is mapped to InMemoryCacheProvider by default.
- You can override ICacheProvider to use Redis as the default.

### 🛠️ Usage
Here’s an example of how you might use the caching provider in a service:

```
public class ProductService
{
    private readonly ICacheProvider _cache;

    public ProductService(ICacheProviderFactory factory)
    {
        _cache = factory.GetProvider(CacheProviderType.Redis); // or CacheProviderType.Memory
    }

    public async Task<Product?> GetProductAsync(string id)
    {
        var key = $"product:{id}";

        var cached = await _cache.GetAsync<Product>(key);
        if (cached != null)
            return cached;

        var product = await FetchFromDbAsync(id); // Hypothetical DB fetch
        if (product != null)
        {
            await _cache.SetAsync(key, product, new CacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }

        return product;
    }
}
```

## 🧩 CacheEntryOptions
Customize expiration per entry:

```
new CacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
    SlidingExpiration = TimeSpan.FromMinutes(5)
}
```
- AbsoluteExpirationRelativeToNow – TTL after which the item expires
- SlidingExpiration – Reset expiration every time the key is accessed

## 🧪 Idempotency Support
Ensure duplicate requests don't reprocess or overwrite data:

```
await _cache.SetAsync("order:123", orderDto, idempotencyKey: "request-abc-xyz", new CacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
});
```
Idempotency key is appended to the main key for uniqueness:
```
Key becomes → order:123:idemp:request-abc-xyz
```

### 🔌 Provider Classes
#### ✅ InMemoryCacheProvider
- Uses IMemoryCache internally
- Ideal for local development or small-scale caching

#### ✅ RedisCacheProvider
- Uses StackExchange.Redis
- Great for distributed, scalable caching
- Requires a running Redis server