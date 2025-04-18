# MeraStore.Shared.Kernel.Context

A lightweight, thread-safe context manager for ASP.NET Core apps.  
Captures per-request metadata like correlation IDs, tenant info, trace IDs, and headers using `AsyncLocal`.

## ✨ Features

- ✅ Thread-safe per-request context using `AsyncLocal`
- ✅ Automatically populates from `HttpContext`
- ✅ Middleware-friendly scope management
- ✅ Includes correlation IDs, tenant ID, trace ID, session ID, etc.
- ✅ Cloning support for background tasks or deferred work
- ✅ Header snapshotting
- ✅ Authorization token capture

---

## 📦 Installation

Reference the library in your **Shared Kernel** layer:

```bash
dotnet add package MeraStore.Shared.Kernel.Context
```

## 🛠️ Usage
### 1. Populate Context (Middleware or Startup)
```
// Example Middleware registration
app.Use(async (context, next) =>
{
    var appContext = AppContextBase.FromHttpContext(context, "your-service-name");
    using (AppContextScope.BeginScope(appContext))
    {
        await next();
    }
});
```
This makes AppContextBase.Current available throughout the lifetime of the request.

### 2. Access Context Anywhere
```
var ctx = AppContextBase.Current;

_logger.LogInformation("CorrelationId: {CorrelationId}", ctx.CorrelationId);

// Or access headers
var userAgent = ctx.Headers["User-Agent"];
```