![](https://cdn.auth0.com/website/sdks/banners/myaccount-net-banner.png)

[![NuGet Version](https://img.shields.io/nuget/v/Auth0.MyAccountApi?style=flat&logo=nuget)](https://www.nuget.org/packages/Auth0.MyAccountApi)
![Downloads](https://img.shields.io/nuget/dt/Auth0.MyAccountApi)
[![License](https://img.shields.io/:license-Apache%202.0-blue.svg?style=flat)](https://www.apache.org/licenses/LICENSE-2.0)

Welcome to the official documentation for the Auth0 MyAccount .NET SDK.

This library provides a strongly-typed .NET client for the [Auth0 My Account API](https://auth0.com/docs/api/myaccount), which lets an authenticated end user manage their own account - enrolling and managing authentication methods (factors) and linking external identity providers as connected accounts.

> [!IMPORTANT]
> Every My Account API call runs **in the context of the signed-in end user**, authorized with that user's access token. This is not a management API: it cannot act on other users, and it is not used with machine-to-machine credentials.

## Features

- 🔐 **End-User Token Management** - supply the current user's access token from any source, resolved per request
- 🧩 **Strongly-Typed Factor Unions** - exhaustive handling of every authentication method type
- 🔑 **Full Enrollment Flows** - email, phone, TOTP, recovery code, password, and passkeys / WebAuthn
- 🔗 **Connected Accounts** - link external identity providers with PKCE support
- 📄 **Cursor-Based Pagination** - iterate list endpoints with `IAsyncEnumerable<T>`
- ⚙️ **Configurable Requests** - per-client and per-request retries, timeouts, and headers
- 🎯 **Typed Error Handling** - typed exceptions with structured, field-level validation errors
- 📦 **Broad Target Support** - .NET 8.0+, .NET Standard 2.0+, and .NET Framework 4.6.2+

## Quick Start

Install the NuGet package:

```sh
dotnet add package Auth0.MyAccountApi
```

Create a client and make your first call:

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

## Documentation Sections

### Getting Started
- [Getting Started](articles/getting-started.md) - Installation and your first request
- [Authentication](articles/authentication.md) - Token providers and ASP.NET Core integration

### Working with the API
- [Authentication Methods](articles/authentication-methods.md) - List, enrol, verify, update, and delete factors
- [Connected Accounts](articles/connected-accounts.md) - Link and manage external identity providers

### Configuration & Usage
- [Configuration](articles/configuration.md) - Request options, retries, timeouts, and base URL
- [Pagination & Error Handling](articles/pagination-and-errors.md) - Paging, raw responses, errors, enums, and null values

### Reference
- [API Reference](api/Auth0.MyAccountApi.yml) - Complete API documentation

## Resources

- [GitHub Repository](https://github.com/auth0/myaccount.net)
- [Auth0 Documentation](https://auth0.com/docs)
- [My Account API Reference](https://auth0.com/docs/api/myaccount)

## License

This project is licensed under the Apache License 2.0 - see the [LICENSE](https://github.com/auth0/myaccount.net/blob/main/LICENSE) file for details.
