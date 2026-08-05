# Getting Started

This guide will help you get started with the Auth0 MyAccount .NET SDK.

## Prerequisites

- An Auth0 account ([sign up for free](https://auth0.com/signup))
- An application configured to request an access token for the My Account API audience (`https://{yourDomain}/me/`), with the scopes matching the operations you intend to call
- A signed-in end user - every call is made on behalf of that user, using their access token
- One of the following supported targets:
  - .NET 8.0+
  - .NET Standard 2.0+
  - .NET Framework 4.6.2+

> [!IMPORTANT]
> The My Account API is **not** a management API. It operates only on the account of the user whose access token is supplied, and it is not used with machine-to-machine client credentials. To manage other users, use the [Auth0 Management API](https://auth0.com/docs/api/management/v2) instead.

### Scopes

Request only the scopes your application needs. Common examples:

| Scope | Grants |
|---|---|
| `read:me:authentication_methods` | List and read the user's authentication methods |
| `create:me:authentication_methods` | Begin enrolling a new authentication method |
| `update:me:authentication_methods` | Rename or reconfigure an authentication method |
| `delete:me:authentication_methods` | Remove an authentication method |
| `read:me:factors` | List factors available for enrollment |
| `read:me:connected_accounts` | List the user's connected accounts |
| `create:me:connected_accounts` | Link a new connected account |
| `delete:me:connected_accounts` | Remove a connected account |

A call made without the required scope fails with a `ForbiddenError` (403). See [Error Handling](pagination-and-errors.md#error-handling).

## Installation

The SDK is available on [NuGet](https://www.nuget.org/packages/Auth0.MyAccountApi) and can be installed via the CLI or Package Manager Console:

```sh
dotnet add package Auth0.MyAccountApi
```

## Your First Request

The recommended entry point is `MyAccountClient`, which wraps the low-level `MyAccountApiClient` and resolves the user's token on each request through an `ITokenProvider`:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>", // e.g. "your-tenant.auth0.com"
    TokenProvider = new DelegateTokenProvider(
        async cancellationToken => await GetCurrentUserAccessTokenAsync(cancellationToken)
    )
});

var methods = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters()
);

foreach (var method in methods.AuthenticationMethods)
{
    Console.WriteLine(method.Type);
}
```

> [!NOTE]
> The Base URL is constructed automatically as `https://{Domain}/me/v1`. The domain must not include a scheme prefix or any path/trailing slash - use `"your-tenant.auth0.com"`, not `"https://your-tenant.auth0.com"` or `"your-tenant.auth0.com/"`. An `ArgumentException` is thrown if an invalid domain value is detected.

Either `Domain` or `BaseUrl` must be set, and `TokenProvider` is required. Omitting them throws at construction time rather than on first request.

## Exploring the Client

The client exposes the My Account API through strongly-typed sub-clients:

```csharp
// Factors enabled for the tenant and available for enrollment.
var factors = await client.Factors.ListAsync();

// The user's own authentication methods.
var methods = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters()
);

// The user's connected external identity providers.
var connectedAccounts = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters()
);
```

| Sub-client | Description |
|---|---|
| `client.Factors` | List factors enabled for the tenant and available for enrollment |
| `client.AuthenticationMethods` | List, enrol, verify, update, and delete the user's authentication methods |
| `client.ConnectedAccounts` | Link, complete, list, and delete accounts connected to external identity providers |
| `client.ConnectedAccounts.Connections` | List connections available for account linking |

## Checking Available Factors

Before offering enrollment, check which factors the tenant has enabled:

```csharp
using Auth0.MyAccountApi;

var response = await client.Factors.ListAsync();

foreach (var factor in response.Factors)
{
    Console.WriteLine($"{factor.Type} - usage: {string.Join(", ", factor.Usage ?? [])}");
}
```

`Factor.Usage` reports whether the factor can be used as a `primary` or `secondary` authentication method.

## Disposing the Client

`MyAccountClient` implements `IDisposable`. If you do not supply an `HttpClient`, the client creates one and disposes it for you:

```csharp
using Auth0.MyAccountApi;

using var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider
});
```

If you supply your own `HttpClient` - the recommended approach in long-running applications - you retain ownership and the SDK will not dispose it. The `ITokenProvider` is never disposed by the client. See [Configuration](configuration.md#httpclient-lifetime).

## Next Steps

- [Authentication](authentication.md) - Token providers and ASP.NET Core integration
- [Authentication Methods](authentication-methods.md) - Enrol and manage factors
- [Connected Accounts](connected-accounts.md) - Link external identity providers
- [Configuration](configuration.md) - Configure retries, timeouts, headers, and the base URL
- [Pagination & Error Handling](pagination-and-errors.md) - Page through lists and handle errors
- [API Reference](../api/Auth0.MyAccountApi.yml) - Complete API documentation
