![C# SDK for Auth0 MyAccount](https://cdn.auth0.com/website/sdks/banners/myaccount-net-banner.png)

<div align="center">

[![NuGet](https://img.shields.io/nuget/v/Auth0.MyAccountApi?style=flat-square)](https://nuget.org/packages/Auth0.MyAccountApi)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Auth0.MyAccountApi?style=flat-square)](https://nuget.org/packages/Auth0.MyAccountApi)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue?style=flat-square)](https://github.com/auth0/myaccount.net/blob/main/LICENSE)


📚 [Documentation](#documentation) • 🚀 [Getting Started](#getting-started) • 💻 [Usage](#usage) • 💬 [Feedback](#feedback)

</div>

---

The Auth0 My Account API SDK for C# provides convenient access to the [Auth0 My Account API](https://auth0.com/docs/api/myaccount). The My Account API lets an authenticated end user manage their own account - enrolling and managing authentication methods (factors) and linking external identity providers as connected accounts. All operations run in the context of the signed-in user's access token.

## Documentation

- [Examples](./EXAMPLES.md) - code samples for every operation and SDK feature.
- [Docs site](https://www.auth0.com/docs) - explore our docs site and learn more about Auth0.
- [API Reference](./reference.md) - complete API reference documentation.

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

### Prerequisites

- An Auth0 account ([sign up for free](https://auth0.com/signup)).
- An application configured to request an access token for the My Account API audience (`https://{yourDomain}/me/`), with the scopes matching the operations you intend to call (for example `read:me:authentication_methods`, `create:me:authentication_methods`, `delete:me:connected_accounts`).
- A signed-in end user. Every My Account API call is made **on behalf of that user**, using their access token - not a machine-to-machine token.

### Your First Request

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

> **Note**
> The Base URL is constructed automatically as `https://{Domain}/me/v1`. The domain must not include a scheme prefix or a path/trailing slash - use `"your-tenant.auth0.com"`, not `"https://your-tenant.auth0.com"` or `"your-tenant.auth0.com/"`. An `ArgumentException` is thrown if an invalid domain value is detected.

### Exploring the Client

The client exposes the My Account API through strongly-typed sub-clients:

| Resource | Description | Examples |
|---|---|---|
| `client.Factors` | List factors enabled for the tenant and available for enrollment. | [Factors](./EXAMPLES.md#factors) |
| `client.AuthenticationMethods` | List, enrol, verify, update, and delete the user's authentication methods. | [Authentication Methods](./EXAMPLES.md#authentication-methods) |
| `client.ConnectedAccounts` | Link, complete, list, and delete accounts connected to external identity providers. | [Connected Accounts](./EXAMPLES.md#connected-accounts) |
| `client.ConnectedAccounts.Connections` | List connections available for account linking. | [Discovering available connections](./EXAMPLES.md#discovering-available-connections) |

---

## Usage

> **Full code samples for everything below live in [EXAMPLES.md](./EXAMPLES.md).** This section summarises the concepts and links to the matching example.

### Authentication

The SDK obtains the end user's access token through an `ITokenProvider`. The provider is invoked on **every** request, so you remain in control of how the token is obtained, cached, and refreshed. `DelegateTokenProvider` covers most cases; implement `ITokenProvider` yourself for custom token-lifecycle logic.

Because the token is resolved per request, a single `MyAccountClient` can be registered as a singleton and still act on behalf of whichever user is making the current request.

| Scenario | Example |
|---|---|
| Supply the token from your own session, cache, or store | [Custom Token Source](./EXAMPLES.md#custom-token-source-recommended) |
| Custom token-lifecycle logic | [Implementing ITokenProvider](./EXAMPLES.md#implementing-itokenprovider) |
| You already hold a bearer token | [Static Token](./EXAMPLES.md#static-token) |
| Register the client in DI and read the token off the current request | [ASP.NET Core Integration](./EXAMPLES.md#aspnet-core-integration) |

### Working With Factors and Authentication Methods

`client.Factors.ListAsync` reports which factors the tenant has enabled and whether each can be used as a `primary` or `secondary` method - check this before offering enrollment. See [Listing Available Factors](./EXAMPLES.md#listing-available-factors).

`client.AuthenticationMethods.ListAsync` returns the signed-in user's methods, optionally filtered by factor type. Each `AuthenticationMethod` is a discriminated union over the supported factor types: use `Visit`/`Match` to handle every case exhaustively, or the `Is*` / `TryAs*` members when you only care about one type. See [Listing Authentication Methods](./EXAMPLES.md#listing-authentication-methods) and [Working With the AuthenticationMethod Union](./EXAMPLES.md#working-with-the-authenticationmethod-union).

Enrollment is always a two-step flow - `CreateAsync` starts it and returns an `Id` plus an `AuthSession`, then `VerifyAsync` confirms it with whatever proof the factor requires. The `CreateAsync` response is a discriminated union whose variant depends on the factor:

| Factor | Create response accessor | Verify payload | Example |
|---|---|---|---|
| Email | `AsMfaBaseCreationResponse()` | `VerifyEmailAuthenticationMethod` | [Email](./EXAMPLES.md#email) |
| Phone | `AsMfaBaseCreationResponse()` | `VerifyPhoneAuthenticationMethod` | [Phone](./EXAMPLES.md#phone-sms-or-voice) |
| Push notification | `AsMfaBaseCreationResponse()` | `VerifyPushNotificationAuthenticationMethod` | - |
| TOTP | `AsQrCodeCreationResponse()` | `VerifyTotpAuthenticationMethod` | [TOTP](./EXAMPLES.md#totp-authenticator-app) |
| Recovery code | `AsRecoveryCodeCreationResponse()` | `VerifyRecoveryCodeAuthenticationMethod` | [Recovery Code](./EXAMPLES.md#recovery-code) |
| Password | `AsPasswordCreationResponse()` | `VerifyPasswordAuthenticationMethod` | [Changing a Password](./EXAMPLES.md#changing-a-password) |
| Passkey | `AsPasskeyCreationResponse()` | `VerifyPasskeyAuthenticationMethod` | [Passkeys and WebAuthn](./EXAMPLES.md#passkeys-and-webauthn) |

`CreateAuthenticationMethodRequestContent` models the seven factor types above. The create response can additionally surface `AsWebAuthnCreationResponse()`, verified with `VerifyWebAuthnPlatformAuthenticationMethod` or `VerifyWebAuthnRoamingAuthenticationMethod`; WebAuthn platform and roaming methods otherwise appear on list and get responses as `AsWebauthnPlatform()` / `AsWebauthnRoaming()`.

Existing methods can be retrieved, renamed (or switched between SMS and voice), and deleted - see [Managing Existing Authentication Methods](./EXAMPLES.md#managing-existing-authentication-methods).

### Connected Accounts

Linking an external identity provider is a two-step, redirect-based flow: `CreateAsync` returns a connect URI and ticket to redirect the user to, and `CompleteAsync` exchanges the connect code from your callback. The `RedirectUri` must match across both steps, and the `CodeVerifier` must match the `CodeChallenge` sent in step 1.

| Task | Example |
|---|---|
| Start the link flow | [1. Start the link flow](./EXAMPLES.md#1-start-the-link-flow) |
| Complete the link flow | [2. Complete the link flow](./EXAMPLES.md#2-complete-the-link-flow) |
| List and remove connected accounts | [Listing and removing](./EXAMPLES.md#listing-and-removing-connected-accounts) |
| Filter by one or many connections | [Filtering by connection](./EXAMPLES.md#filtering-by-connection) |
| Find out which connections the user can link | [Discovering available connections](./EXAMPLES.md#discovering-available-connections) |

### Pagination

The connected-accounts list endpoints are cursor-paginated and return a `Pager<T>`, which implements `IAsyncEnumerable<T>` - `await foreach` over it and the SDK fetches subsequent pages transparently. You can also iterate `AsPagesAsync()` a page at a time, or drive the cursor manually via `CurrentPage`, `HasNextPage`, and `GetNextPageAsync()`. See [Pagination](./EXAMPLES.md#pagination).

> **Note**
> `Take` accepts values from 1 to 20 and defaults to 10. `client.AuthenticationMethods.ListAsync` and `client.Factors.ListAsync` are not paginated - they return the full collection.

### Configuration

Options can be configured at the client level (affecting all requests) or per request. See [Request Options](./EXAMPLES.md#request-options) for a worked example, and the dedicated examples for [Retries](./EXAMPLES.md#retries), [Timeouts](./EXAMPLES.md#timeouts), [Base URL](./EXAMPLES.md#base-url), [Cancellation](./EXAMPLES.md#cancellation), and [HttpClient Lifetime](./EXAMPLES.md#httpclient-lifetime).

Client options (`MyAccountClientOptions`):

| Option | Description |
|---|---|
| `TokenProvider` | **Required.** Supplies the end user's access token on every request. |
| `Domain` | Auth0 tenant domain (e.g. `"your-tenant.auth0.com"`); used to construct the base URL as `https://{Domain}/me/v1` |
| `BaseUrl` | Override the base URL directly; takes precedence over `Domain` |
| `HttpClient` | Provide a custom `HttpClient` |
| `AdditionalHeaders` | Additional HTTP headers sent with every request |
| `MaxRetries` | Maximum retry attempts (default `2`) |
| `Timeout` | Request timeout (default 30 seconds) |

Per-request options (`RequestOptions`):

| Option | Description |
|---|---|
| `MaxRetries` | Maximum retry attempts for this request |
| `Timeout` | Request timeout |
| `AdditionalHeaders` | Additional HTTP headers to send |
| `AdditionalQueryParameters` | Additional query parameters to append |
| `AdditionalBodyProperties` | Additional JSON body properties |
| `BaseUrl` | Override the base URL for this request |
| `HttpClient` | Override the `HttpClient` for this request |

Requests are retried automatically with exponential backoff and jitter on `408`, `429`, and `5XX` responses. The `Retry-After` and `X-RateLimit-Reset` headers are respected when present, and the delay is capped at 60 seconds.

### Error Handling

When the API returns a non-success status code (4xx or 5xx), the SDK throws a typed subclass of `MyAccountApiException`. Each subclass exposes a strongly-typed `ErrorResponse` body, so validation failures can be surfaced field by field - see [Error Handling](./EXAMPLES.md#error-handling).

| Type | Status Code | Description |
|---|---|---|
| `BadRequestError` | 400 | Invalid request |
| `UnauthorizedError` | 401 | Token missing, invalid, or expired |
| `ForbiddenError` | 403 | Insufficient scope |
| `NotFoundError` | 404 | Resource not found |
| `UnsupportedMediaTypeError` | 415 | Unsupported request content type |
| `TooManyRequestsError` | 429 | Rate limit exceeded |

All of the above derive from `MyAccountApiException`, which in turn derives from `MyAccountException` - the base type for every exception this SDK raises.

`ErrorResponse` exposes `Type`, `Status`, `Title`, `Detail`, and an optional `ValidationErrors` collection, where each `ValidationError` carries `Detail`, `Field`, `Pointer`, and `Source`.

### Raw Responses

Use `.WithRawResponse()` to access the status code, URL, and headers alongside the parsed response data. See [Raw Responses](./EXAMPLES.md#raw-responses).

### Explicit Null Values

By default, fields with `null` values are omitted from the request. Query parameters modelled as `Optional<T?>` let you distinguish "omit this parameter" from "send it explicitly as null" - for example to opt out of the default `Take = 10`. Assigning a value directly also works; an implicit conversion wraps it for you. See [Explicit Null Values](./EXAMPLES.md#explicit-null-values).

### Forward Compatible Enums

This SDK uses forward-compatible enums that handle unknown values gracefully, so new server-side values won't break your code. Use `FromCustom` for values the SDK doesn't know about, and switch on `.Value` with a `default` branch. See [Forward Compatible Enums](./EXAMPLES.md#forward-compatible-enums).

## API Reference

The full API reference is available in [reference.md](./reference.md).

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
