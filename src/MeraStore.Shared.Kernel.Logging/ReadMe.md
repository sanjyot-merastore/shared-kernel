# MeraStore.Shared.Kernel.Logging

A flexible and extensible logging SDK for the MeraStore platform, supporting structured logging with multiple sinks (Console, File, Elasticsearch, etc.), pluggable masking, and contextual enrichment.

---

## ✨ Features

- Fluent `LoggerBuilder` for configuring sinks and behavior
- Support for multiple log sinks:
  - Console
  - File
  - Elasticsearch
- Centralized log dispatching via `LogWriter`
- Custom field injection (e.g., `service-name`)
- Masking support via `IMasker` interface (e.g., Email, Credit Card)
- Integration-ready with `Serilog`

---

## 📦 Installation

This package is part of the `MeraStore.Shared.Kernel` monorepo. Add the reference to your project:

```bash
dotnet add reference ../MeraStore.Shared.Kernel.Logging/MeraStore.Shared.Kernel.Logging.csproj
```

## 🛠️ Usage

#### 1. Configure Logger
```csharp
builder.AddLoggingServices("SampleApi", options =>
{
    options.UseConsole = true;
    options.UseFile = true;
    options.UseElasticsearch = true;
    options.ElasticsearchUrl = builder.Configuration["Logging:Elasticsearch:Url"];
    options.UseInfrastructureSink = true;
    options.UseEntityFrameworkSink = false;
});

```

#### 2. Log Messages
```csharp
   await Logger.LogAsync(new ApiLog("This is form async logWriter"));
   Logger.Log(new TraceLog("This is form sync logWriter"));
```

## 🧪 Testing
### Unit tests should verify:

- Sink output behavior
- Log field enrichment
- Masking application
- Multi-sink coordination

### 📁 Project Structure
```
MeraStore.Shared.Kernel.Logging/
├── Interfaces/
│   └── ILogSink.cs
├── Sinks/
│   ├── ConsoleLogSink.cs
│   ├── FileLogSink.cs
│   └── ElasticSearch/
│       └── ApplicationSink.cs
├── Masking/
│   ├── IMasker.cs
│   └── EmailMasker.cs (example)
├── LoggerBuilder.cs
├── LogWriter.cs
└── LoggingConstants.cs
```