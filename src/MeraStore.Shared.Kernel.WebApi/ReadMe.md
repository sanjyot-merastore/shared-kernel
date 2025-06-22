# MeraStore.Shared.Kernel.WebApi

> 💼 **Part of the MeraStore Shared Kernel Suite**  
> 🛠️ Middleware & utilities to make your Web API life easier, cleaner, and beautifully consistent.

---

## ✨ Features

- ✅ **Error Handling Middleware** – Consistent problem responses for unhandled exceptions.
- 📜 **Request/Response Logging** – Plug in to structured logs with masking support (integrates with MeraStore.Logging).
- ⛔ **Rate Limiting** – Middleware to throttle abusive clients or APIs.
- 🧼 **Model Validation Middleware & Filters** – Automatic 400s on invalid models.
- 🔌 **Extensibility Hooks** – Easily extend the pipeline with your own logic.
- 🧪 **Ready-to-use Filters & Attributes** – DRY your controllers with custom decorators.

---

## 📁 Project Structure

```
│
├── Middleware/
│ ├── ErrorHandlingMiddleware.cs # Global exception handler
│ ├── RequestLoggingMiddleware.cs # Logs requests & responses
│ ├── RateLimitingMiddleware.cs # Controls request rate per client
│ └── ValidationMiddleware.cs # Validates models on entry
│
├── Extensions/
│ ├── ApplicationBuilderExtensions.cs # AddMiddlewareChain()
│ └── ServiceCollectionExtensions.cs # AddWebApiInfrastructure()
│
├── Models/
│ └── ErrorResponse.cs # Standard error response structure
│
├── Attributes/
│ └── ValidateModelAttribute.cs # Decorator to enforce model state validation
│
├── Filters/
│ └── ValidationFilter.cs # Action filter for model validation
│
├── Helpers/
│ └── RateLimitHelper.cs # Token bucket or sliding window helper

```