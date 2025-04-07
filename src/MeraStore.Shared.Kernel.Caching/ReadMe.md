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


### 🛠️ Usage
Here’s an example of how you might use the caching provider in a service:

```
public class ProductService
{
    private readonly ICacheProvider _cache;

    public ProductService(ICacheProviderFactory factory)
    {
        _cache = factory.GetProvider(CacheProviderType.Redis); // or "memory"
    }

    public async Task<Product?> GetProductAsync(string id)
    {
        var key = $"product:{id}";

        var cached = await _cache.GetAsync<Product>(key);
        if (cached != null)
            return cached;

        var product = await FetchFromDbAsync(id);
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