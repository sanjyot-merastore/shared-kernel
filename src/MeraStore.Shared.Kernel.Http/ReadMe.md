﻿# MeraStore.Shared.Kernel.Http

A streamlined and extensible HTTP client library for the MeraStore platform, designed to simplify and standardize HTTP communications across microservices. Built with resilience, observability, and logging in mind.

---

## ✨ Features

- Fluent `HttpRequestBuilder` for composing HTTP requests
- Built-in support for:
  - GET, POST, PUT, DELETE
  - Query parameters and headers
  - JSON body serialization/deserialization
- Logging integration via `MeraStore.Shared.Kernel.Logging`
  - API request/response logging
  - Custom logging fields
  - Masking of sensitive payloads
- Retry and timeout support (coming soon)
- Pluggable pipeline for request/response interception

---

## 📦 Installation

This package is part of the `MeraStore.Shared.Kernel` monorepo. Add the reference to your project:

## 🛠️ Usage

### 1 Vanilla HTTP Request

```csharp

           var builder = new HttpRequestBuilder()
              .WithMethod(HttpMethod.Get)
              .WithUri(url)
              .WithRequestId(AppContext.Current.RequestId)
              .WithCorrelationId(AppContext.Current.CorrelationId)
              .WithTimeout(TimeSpan.FromSeconds(15))
              .UseDefaultResilience()
              .WithLoggingField("controller", "LoggingApi")
              .WithLoggingField("action", "Get_Request_Payload".ToLower())
              .Build();

            var response = await builder.SendAsync(cancellationToken: cancellationToken);

```


### 2 With a Wrapper
```csharp


```
