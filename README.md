![C# SDK for Auth0 MyAccount](https://cdn.auth0.com/website/sdks/banners/myaccount-net-banner.png)

<div align="center">

[![NuGet](https://img.shields.io/nuget/v/Auth0.MyAccountApi?style=flat-square)](https://nuget.org/packages/Auth0.MyAccountApi)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Auth0.MyAccountApi?style=flat-square)](https://nuget.org/packages/Auth0.MyAccountApi)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue?style=flat-square)](https://github.com/auth0/myaccount.net/blob/main/LICENSE)


📚 [Documentation](#documentation) • 🚀 [Getting Started](#getting-started) • 💬 [Feedback](#feedback)

</div>

---

The Auth0 My Account API SDK for C# provides convenient access to the [Auth0 My Account API](https://auth0.com/docs/api/myaccount). The My Account API lets an authenticated end user manage their own account — enrolling and managing authentication methods (factors) and linking external identity providers as connected accounts. All operations run in the context of the signed-in user's access token.

## Documentation

- [Docs site](https://www.auth0.com/docs) — explore our docs site and learn more about Auth0.
- [API Reference](./reference.md) — complete API reference documentation.

## Getting Started

### Requirements

This library supports the following targets:

- .NET 8.0+
- .NET Standard 2.0+
- .NET Framework 4.6.2+

### Installation

The SDK is available on [NuGet](https://www.nuget.org/packages/Auth0.MyAccountApi) and can be installed via the CLI or Package Manager Console:

```sh
dotnet add package Auth0.MyAccountApi
```

### Usage

Because the My Account API operates on behalf of the signed-in end user, every request is authorized with that user's access token. The recommended entry point is `MyAccountClient`, which wraps the low-level `MyAccountApiClient` and resolves the user's token on each request through an `ITokenProvider`.

#### Custom Token Source (recommended)

Use `DelegateTokenProvider` to supply the current user's access token from your own source — an in-memory session, a secure store, a cache, or a refresh-token exchange. The delegate is invoked on every request, so you remain in control of how the token is obtained and refreshed.

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>", // e.g. "your-tenant.auth0.com"
    TokenProvider = new DelegateTokenProvider(
        async cancellationToken => await GetCurrentUserAccessTokenAsync(cancellationToken)
    )
});

var authenticationMethods = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters()
);
```

> **Note**
> The Base URL is constructed automatically as `https://{Domain}/me/v1`. The domain must not include a scheme prefix or a path/trailing slash — use `"your-tenant.auth0.com"`, not `"https://your-tenant.auth0.com"` or `"your-tenant.auth0.com/"`. An `ArgumentException` is thrown if an invalid domain value is detected.

Implement `ITokenProvider` directly if you need custom token-lifecycle logic beyond a delegate:

```csharp
using Auth0.MyAccountApi;

public sealed class MyTokenProvider : ITokenProvider
{
    public Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        // Your custom logic to obtain (and cache/refresh) the user's access token.
        return Task.FromResult("<USER_ACCESS_TOKEN>");
    }
}
```

#### Static Token

If you already hold a bearer token, the recommended approach is still `MyAccountClient` with a `DelegateTokenProvider`. This keeps token handling consistent and gives you access to all wrapper features such as retries and timeouts:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("<USER_ACCESS_TOKEN>"))
});
```

Alternatively, you can use `MyAccountApiClient` directly. This is the underlying generated client and accepts a static token without any token lifecycle management — the token will not be refreshed automatically. This approach is only recommended for advanced scenarios such as test harnesses, middleware, or environments that inject tokens externally:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountApiClient(
    token: "<USER_ACCESS_TOKEN>",
    clientOptions: new ClientOptions
    {
        BaseUrl = "https://<YOUR_AUTH0_DOMAIN>/me/v1"
    }
);
```

### Working with Authentication Methods

The `AuthenticationMethods` resource lets the user list, enrol, verify, update, and delete their factors.

```csharp
using Auth0.MyAccountApi;

// List authentication methods, optionally filtered by factor type.
var methods = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters { Type = FactorTypeEnum.Password }
);

// Start enrollment of a new factor (here, a phone authentication method).
await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Phone(
            new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
        )
    )
);

// Confirm enrollment.
await client.AuthenticationMethods.VerifyAsync(
    "authentication_method_id",
    new VerifyEmailAuthenticationMethod { AuthSession = "Fe26.2**05c400ed...", OtpCode = "123456" }
);
```

### Pagination

List endpoints that return collections are paginated. The SDK returns a `Pager<T>`, which implements `IAsyncEnumerable<T>`, so you can iterate every item across pages with `await foreach`:

```csharp
using Auth0.MyAccountApi;

var connectedAccounts = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters
    {
        Connection = "connection",
        From = "from",
        Take = 1,
    }
);

await foreach (var connectedAccount in connectedAccounts)
{
    // Do something with connectedAccount.
}
```

### Request Options

`RequestOptions` can be passed per request to override client-level configuration:

```csharp
using Auth0.MyAccountApi;

var method = await client.AuthenticationMethods.GetAsync(
    "authentication_method_id",
    new RequestOptions
    {
        MaxRetries = 5,                       // More retries for this call.
        Timeout = TimeSpan.FromSeconds(3),
        AdditionalHeaders = new Dictionary<string, string?>
        {
            { "X-Request-Id", "abc-123" }
        }
    }
);
```

Available request options:

| Option | Description |
|---|---|
| `MaxRetries` | Maximum retry attempts for this request |
| `Timeout` | Request timeout |
| `AdditionalHeaders` | Additional HTTP headers to send |
| `AdditionalQueryParameters` | Additional query parameters to append |

### Raw Responses

Access raw HTTP response data (status code, headers, URL) alongside parsed response data using the `.WithRawResponse()` method:

```csharp
using Auth0.MyAccountApi;

var result = await client.AuthenticationMethods
    .ListAsync(new ListAuthenticationMethodsRequestParameters())
    .WithRawResponse();

// Access the parsed data.
var data = result.Data;

// Access raw response metadata.
Console.WriteLine($"Status: {(int)result.RawResponse.StatusCode}");
Console.WriteLine($"URL: {result.RawResponse.Url}");

// Access specific headers (case-insensitive).
if (result.RawResponse.Headers.TryGetValue("X-Request-Id", out var requestId))
{
    Console.WriteLine($"Request ID: {requestId}");
}
```

`RawResponse` exposes `StatusCode` (`HttpStatusCode`), `Url` (`Uri`), and `Headers` (`ResponseHeaders`).

### Error Handling

When the API returns a non-success status code (4xx or 5xx), the SDK throws a typed subclass of `MyAccountApiException`. These can be caught using standard C# exception handling:

```csharp
using Auth0.MyAccountApi;

try
{
    var response = await client.AuthenticationMethods.CreateAsync(request);
}
catch (NotFoundError e)
{
    Console.WriteLine(e.Body);
    Console.WriteLine(e.StatusCode);
}
catch (UnauthorizedError e)
{
    Console.WriteLine(e.Body);
}
catch (MyAccountApiException e)
{
    // Catch any other API error.
    Console.WriteLine($"Status: {e.StatusCode}");
    Console.WriteLine(e.Body);

    // Access the raw HTTP response (status code, URL, headers) off the exception.
    var rawResponse = e.RawResponse;
    if (rawResponse != null)
    {
        Console.WriteLine(rawResponse.Url);
        if (rawResponse.Headers.TryGetValue("X-Request-Id", out var requestId))
        {
            Console.WriteLine($"Request ID: {requestId}");
        }
    }
}
```

Available error types:

| Type | Status Code | Description |
|---|---|---|
| `BadRequestError` | 400 | Invalid request |
| `UnauthorizedError` | 401 | Token missing, invalid, or expired |
| `ForbiddenError` | 403 | Insufficient scope |
| `NotFoundError` | 404 | Resource not found |
| `UnsupportedMediaTypeError` | 415 | Unsupported request content type |
| `TooManyRequestsError` | 429 | Rate limit exceeded |

### Forward Compatible Enums

This SDK uses forward-compatible enums that can handle unknown values gracefully. This means new server-side values won't break your code:

```csharp
using Auth0.MyAccountApi;

// Using a built-in value.
var factorType = FactorTypeEnum.Password;

// Using a custom value.
var customFactorType = FactorTypeEnum.FromCustom("custom-value");

// Using in a switch statement.
switch (factorType.Value)
{
    case FactorTypeEnum.Values.Password:
        Console.WriteLine("Password");
        break;
    default:
        Console.WriteLine($"Unknown value: {factorType.Value}");
        break;
}

// Explicit casting.
string factorTypeString = (string)FactorTypeEnum.Password;
FactorTypeEnum factorTypeFromString = (FactorTypeEnum)"password";
```

### Retries

The SDK automatically retries requests with exponential backoff on the following status codes:

- `408` (Timeout)
- `429` (Too Many Requests)
- `5XX` (Server Errors)

The default retry limit is 2 attempts. Configure this via the `MaxRetries` option:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    MaxRetries = 5
});
```

You can also override `MaxRetries` per request via `RequestOptions`.

### Timeouts

The SDK defaults to a 30 second timeout. Configure this via the `Timeout` option:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    Timeout = TimeSpan.FromSeconds(10)
});
```

You can also override `Timeout` per request via `RequestOptions`.

### Base URL

When `Domain` is set, the Base URL defaults to `https://{Domain}/me/v1`. Set `BaseUrl` directly to override this behaviour — for example, when targeting a custom domain or a local test server. When both are provided, `BaseUrl` takes precedence.

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    BaseUrl = "https://custom.example.com/me/v1",
    TokenProvider = tokenProvider
});
```

## API Reference

The full API reference is available in [reference.md](./reference.md). The client exposes the following resources:

| Resource | Description |
|---|---|
| `client.Factors` | List factors enabled for the tenant and available for enrollment. |
| `client.AuthenticationMethods` | List, enrol, verify, update, and delete the user's authentication methods. |
| `client.ConnectedAccounts` | Link, complete, list, and delete accounts connected to external identity providers. |
| `client.ConnectedAccounts.Connections` | List connections available for account linking. |

## Feedback

### Contributing

We appreciate feedback and contribution to this repo! Before you get started, please see the [contributing guidelines](./CONTRIBUTING.md).

While we value open-source contributions to this SDK, this library is generated programmatically. Additions made directly to this library would have to be moved over to our generation code, otherwise they would be overwritten upon the next generated release. Feel free to open a PR as a proof of concept, but know that we will not be able to merge it as-is. We suggest opening an issue first to discuss with us!

### Raise an Issue

To provide feedback or report a bug, please [raise an issue on our issue tracker](https://github.com/auth0/myaccount.net/issues).

### Vulnerability Reporting

Please do not report security vulnerabilities on the public GitHub issue tracker. The [Responsible Disclosure Program](https://auth0.com/responsible-disclosure-policy) details the procedure for disclosing security issues.

---

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: light)" srcset="https://cdn.auth0.com/website/sdks/logos/auth0_light_mode.png" width="150">
    <source media="(prefers-color-scheme: dark)" srcset="https://cdn.auth0.com/website/sdks/logos/auth0_dark_mode.png" width="150">
    <img alt="Auth0 Logo" src="https://cdn.auth0.com/website/sdks/logos/auth0_light_mode.png" width="150">
  </picture>
</p>

<p align="center">Auth0 is an easy to implement, adaptable authentication and authorization platform.<br />To learn more check out <a href="https://auth0.com/why-auth0">Why Auth0?</a></p>

<p align="center">Copyright 2026 Okta, Inc. <br>
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. <br> You may obtain a copy of the License at <a href="http://www.apache.org/licenses/LICENSE-2.0"> http://www.apache.org/licenses/LICENSE-2.0</a></p>
