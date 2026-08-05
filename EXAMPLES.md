# Examples

Practical, copy-pasteable examples for the [Auth0 My Account API SDK for C#](./README.md).

Unless an example constructs its own client, `client` refers to a configured `MyAccountClient` as shown in [Your First Request](./README.md#your-first-request), and every snippet assumes `using Auth0.MyAccountApi;`.

## Table of Contents

- [Authentication](#authentication)
  - [Custom Token Source (recommended)](#custom-token-source-recommended)
  - [Implementing ITokenProvider](#implementing-itokenprovider)
  - [Static Token](#static-token)
  - [ASP.NET Core Integration](#aspnet-core-integration)
- [Factors](#factors)
  - [Listing Available Factors](#listing-available-factors)
- [Authentication Methods](#authentication-methods)
  - [Listing Authentication Methods](#listing-authentication-methods)
  - [Working With the AuthenticationMethod Union](#working-with-the-authenticationmethod-union)
  - [Enrolling Authentication Methods](#enrolling-authentication-methods)
    - [Email](#email)
    - [Phone (SMS or voice)](#phone-sms-or-voice)
    - [TOTP (authenticator app)](#totp-authenticator-app)
    - [Recovery Code](#recovery-code)
    - [Changing a Password](#changing-a-password)
    - [Passkeys and WebAuthn](#passkeys-and-webauthn)
  - [Managing Existing Authentication Methods](#managing-existing-authentication-methods)
- [Connected Accounts](#connected-accounts)
  - [1. Start the link flow](#1-start-the-link-flow)
  - [2. Complete the link flow](#2-complete-the-link-flow)
  - [Listing and removing connected accounts](#listing-and-removing-connected-accounts)
  - [Filtering by connection](#filtering-by-connection)
  - [Discovering available connections](#discovering-available-connections)
- [Pagination](#pagination)
- [Request Options](#request-options)
- [Cancellation](#cancellation)
- [Raw Responses](#raw-responses)
- [Error Handling](#error-handling)
- [Explicit Null Values](#explicit-null-values)
- [Forward Compatible Enums](#forward-compatible-enums)
- [Retries](#retries)
- [Timeouts](#timeouts)
- [Base URL](#base-url)
- [HttpClient Lifetime](#httpclient-lifetime)
- [Telemetry](#telemetry)

---

## Authentication

The SDK obtains the end user's access token through an `ITokenProvider`. The provider is invoked on **every** request, so you remain in control of how the token is obtained, cached, and refreshed.

### Custom Token Source (recommended)

Use `DelegateTokenProvider` to supply the current user's access token from your own source - an in-memory session, a secure store, a cache, or a refresh-token exchange:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = new DelegateTokenProvider(async cancellationToken =>
    {
        // Your custom logic to obtain the signed-in user's access token.
        return await GetCurrentUserAccessTokenAsync(cancellationToken);
    })
});
```

> **Note**
> The delegate must return a non-empty token. `DelegateTokenProvider` throws an `InvalidOperationException` if it returns null or whitespace.

### Implementing ITokenProvider

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

### Static Token

If you already hold a bearer token, the recommended approach is still `MyAccountClient` with a `DelegateTokenProvider`. This keeps token handling consistent and gives you access to all wrapper features such as retries and timeouts:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("<USER_ACCESS_TOKEN>"))
});
```

Alternatively, you can use `MyAccountApiClient` directly. This is the underlying generated client and accepts a static token without any token lifecycle management - the token will not be refreshed automatically. This approach is only recommended for advanced scenarios such as test harnesses, middleware, or environments that inject tokens externally:

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

### ASP.NET Core Integration

Because the token provider is resolved per request, a single `MyAccountClient` can be registered as a singleton and still act on behalf of whichever user is making the current request:

```csharp
using Auth0.MyAccountApi;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

    return new MyAccountClient(new MyAccountClientOptions
    {
        Domain = builder.Configuration["Auth0:Domain"]!,
        HttpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("auth0-myaccount"),
        TokenProvider = new DelegateTokenProvider(async _ =>
        {
            var httpContext = httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("No active HTTP request.");

            return await httpContext.GetTokenAsync("access_token")
                ?? throw new InvalidOperationException("No access token on the current request.");
        })
    });
});

var app = builder.Build();

app.MapGet("/me/authentication-methods", async (MyAccountClient client) =>
{
    var methods = await client.AuthenticationMethods.ListAsync(
        new ListAuthenticationMethodsRequestParameters()
    );

    return Results.Ok(methods.AuthenticationMethods);
});

app.Run();
```

---

## Factors

### Listing Available Factors

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

---

## Authentication Methods

### Listing Authentication Methods

`ListAsync` returns every authentication method belonging to the signed-in user, optionally filtered by factor type:

```csharp
using Auth0.MyAccountApi;

// All authentication methods.
var all = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters()
);

// Filtered by factor type.
var phones = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters { Type = FactorTypeEnum.Phone }
);
```

### Working With the AuthenticationMethod Union

Each `AuthenticationMethod` is a discriminated union over the supported factor types. Use `Visit` (or `Match` to return a value) to handle each case exhaustively - the `onUnknown_` branch keeps your code working when Auth0 introduces new factor types:

```csharp
foreach (var method in all.AuthenticationMethods)
{
    method.Visit(
        onPassword: password => Console.WriteLine($"Password (identity {password.IdentityUserId})"),
        onPasskey: passkey => Console.WriteLine($"Passkey {passkey.Id}"),
        onRecoveryCode: recoveryCode => Console.WriteLine($"Recovery code {recoveryCode.Id}"),
        onPushNotification: push => Console.WriteLine($"Push {push.Id}"),
        onTotp: totp => Console.WriteLine($"TOTP {totp.Id} (confirmed: {totp.Confirmed})"),
        onWebauthnPlatform: platform => Console.WriteLine($"WebAuthn platform {platform.Id}"),
        onWebauthnRoaming: roaming => Console.WriteLine($"WebAuthn roaming {roaming.Id}"),
        onPhone: phone => Console.WriteLine($"Phone {phone.PhoneNumber}"),
        onEmail: email => Console.WriteLine($"Email {email.Email}"),
        onUnknown_: (type, value) => Console.WriteLine($"Unknown factor type: {type}")
    );
}
```

If you only care about one or two types, use the `Is*` properties or the `TryAs*` methods instead:

```csharp
foreach (var method in all.AuthenticationMethods)
{
    if (method.IsPhone)
    {
        var phone = method.AsPhone();
        Console.WriteLine($"{phone.PhoneNumber} via {phone.PreferredAuthenticationMethod}");
    }

    if (method.TryAsEmail(out var email))
    {
        Console.WriteLine($"{email!.Email} confirmed: {email.Confirmed}");
    }
}
```

> **Note**
> `As*` throws if the method is not of the requested type. Prefer `TryAs*` when the type is not already known.

### Enrolling Authentication Methods

Enrollment is always a two-step flow:

1. `CreateAsync` starts the enrollment and returns an `Id` plus an `AuthSession`.
2. `VerifyAsync` confirms it using the `AuthSession` and whatever proof the factor requires.

The `CreateAsync` response is a discriminated union - the variant depends on the factor being enrolled. The table below maps each factor to the response accessor and the verify payload to use:

| Factor | Create response accessor | Verify payload | Example |
|---|---|---|---|
| Email | `AsMfaBaseCreationResponse()` | `VerifyEmailAuthenticationMethod` | [Email](#email) |
| Phone | `AsMfaBaseCreationResponse()` | `VerifyPhoneAuthenticationMethod` | [Phone](#phone-sms-or-voice) |
| Push notification | `AsMfaBaseCreationResponse()` | `VerifyPushNotificationAuthenticationMethod` | - |
| TOTP | `AsQrCodeCreationResponse()` | `VerifyTotpAuthenticationMethod` | [TOTP](#totp-authenticator-app) |
| Recovery code | `AsRecoveryCodeCreationResponse()` | `VerifyRecoveryCodeAuthenticationMethod` | [Recovery Code](#recovery-code) |
| Password | `AsPasswordCreationResponse()` | `VerifyPasswordAuthenticationMethod` | [Password](#changing-a-password) |
| Passkey | `AsPasskeyCreationResponse()` | `VerifyPasskeyAuthenticationMethod` | [Passkey](#passkeys-and-webauthn) |

`CreateAuthenticationMethodRequestContent` models the seven factor types above. The create response can additionally surface `AsWebAuthnCreationResponse()`, verified with `VerifyWebAuthnPlatformAuthenticationMethod` or `VerifyWebAuthnRoamingAuthenticationMethod`; WebAuthn platform and roaming methods otherwise appear on list and get responses as `AsWebauthnPlatform()` / `AsWebauthnRoaming()`.

#### Email

```csharp
using Auth0.MyAccountApi;

// 1. Start enrollment - Auth0 sends a one-time code to the email address.
var created = await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Email(
            new CreateEmailAuthenticationMethod { Email = "user@example.com" }
        )
    )
);

var enrollment = created.AsMfaBaseCreationResponse();

// 2. Confirm enrollment with the code the user received.
var verified = await client.AuthenticationMethods.VerifyAsync(
    enrollment.Id,
    new VerifyEmailAuthenticationMethod
    {
        AuthSession = enrollment.AuthSession,
        OtpCode = "123456"
    }
);

Console.WriteLine(verified.AsEmail().Confirmed);
```

#### Phone (SMS or voice)

```csharp
using Auth0.MyAccountApi;

var created = await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Phone(
            new CreatePhoneAuthenticationMethod
            {
                PhoneNumber = "+15551234567",
                PreferredAuthenticationMethod = PhoneAuthenticationMethodEnum.Sms
            }
        )
    )
);

var enrollment = created.AsMfaBaseCreationResponse();

await client.AuthenticationMethods.VerifyAsync(
    enrollment.Id,
    new VerifyPhoneAuthenticationMethod
    {
        AuthSession = enrollment.AuthSession,
        OtpCode = "123456"
    }
);
```

#### TOTP (authenticator app)

TOTP enrollment returns a QR code URI to display to the user:

```csharp
using Auth0.MyAccountApi;

var created = await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Totp(
            new CreateTotpAuthenticationMethod()
        )
    )
);

var qr = created.AsQrCodeCreationResponse();

// Render qr.BarcodeUri as a QR code, or show qr.ManualInputCode as a fallback.
Console.WriteLine(qr.BarcodeUri);
Console.WriteLine(qr.ManualInputCode);

await client.AuthenticationMethods.VerifyAsync(
    qr.Id,
    new VerifyTotpAuthenticationMethod
    {
        AuthSession = qr.AuthSession,
        OtpCode = "123456"
    }
);
```

#### Recovery Code

```csharp
using Auth0.MyAccountApi;

var created = await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.RecoveryCode(
            new CreateRecoveryCodeAuthenticationMethod()
        )
    )
);

var recovery = created.AsRecoveryCodeCreationResponse();

// Show recovery.RecoveryCode to the user once - it cannot be retrieved again.
Console.WriteLine(recovery.RecoveryCode);

await client.AuthenticationMethods.VerifyAsync(
    recovery.Id,
    new VerifyRecoveryCodeAuthenticationMethod { AuthSession = recovery.AuthSession }
);
```

#### Changing a Password

Password changes follow the same create/verify shape. The create response carries the tenant's password policy, so you can validate input before submitting:

```csharp
using Auth0.MyAccountApi;

var created = await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Password(
            new CreatePasswordAuthenticationMethod()
        )
    )
);

var session = created.AsPasswordCreationResponse();

// Inspect the tenant password policy before prompting the user.
Console.WriteLine(session.Policy);

await client.AuthenticationMethods.VerifyAsync(
    session.Id,
    new VerifyPasswordAuthenticationMethod
    {
        AuthSession = session.AuthSession,
        NewPassword = "<NEW_PASSWORD>"
    }
);
```

#### Passkeys and WebAuthn

Passkey enrollment requires a browser round-trip: Auth0 returns WebAuthn creation options, the browser produces an attestation, and you post that attestation back:

```csharp
using Auth0.MyAccountApi;

// 1. Start enrollment - returns WebAuthn creation options for the browser.
var created = await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Passkey(
            new CreatePasskeyAuthenticationMethod()
        )
    )
);

var passkey = created.AsPasskeyCreationResponse();
var options = passkey.AuthnParamsPublicKey;

// 2. Send `options` to the browser and call navigator.credentials.create().
Console.WriteLine($"Challenge: {options.Challenge}, RP: {options.Rp.Id}");

// 3. Post the attestation returned by the browser back to Auth0.
await client.AuthenticationMethods.VerifyAsync(
    "<AUTHENTICATION_METHOD_ID>",
    new VerifyPasskeyAuthenticationMethod
    {
        AuthSession = passkey.AuthSession,
        AuthnResponse = new AuthenticatorAttestationResponse
        {
            Id = "<CREDENTIAL_ID>",
            RawId = "<CREDENTIAL_RAW_ID>",
            Type = AuthenticatorAttestationResponseType.PublicKey,
            AuthenticatorAttachment = AuthenticatorAttachmentEnum.Platform,
            Response = new AuthenticatorAttestationResponseData
            {
                AttestationObject = "<ATTESTATION_OBJECT>",
                ClientDataJson = "<CLIENT_DATA_JSON>"
            }
        }
    }
);
```

### Managing Existing Authentication Methods

```csharp
using Auth0.MyAccountApi;

// Retrieve a single method.
var method = await client.AuthenticationMethods.GetAsync("<AUTHENTICATION_METHOD_ID>");

// Rename it, or switch a phone factor between SMS and voice.
await client.AuthenticationMethods.UpdateAsync(
    "<AUTHENTICATION_METHOD_ID>",
    new UpdateAuthenticationMethodRequestContent
    {
        Name = "Work phone",
        PreferredAuthenticationMethod = PhoneAuthenticationMethodEnum.Voice
    }
);

// Remove it.
await client.AuthenticationMethods.DeleteAsync("<AUTHENTICATION_METHOD_ID>");
```

> **Note**
> The authentication method ID is returned in the `Location` header when creating a method, and as the `Id` property on list and create responses. Use it exactly as returned, without modification.

---

## Connected Accounts

Linking an external identity provider is a two-step, redirect-based flow.

### 1. Start the link flow

```csharp
using Auth0.MyAccountApi;

var ticket = await client.ConnectedAccounts.CreateAsync(
    new CreateConnectedAccountsRequestContent
    {
        Connection = "google-oauth2",
        RedirectUri = "https://app.example.com/callback",
        Scopes = ["openid", "profile", "email", "offline_access"],
        State = "<OPAQUE_STATE>",
        CodeChallenge = "<PKCE_CODE_CHALLENGE>",
        CodeChallengeMethod = CreateConnectedAccountsRequestContentCodeChallengeMethod.S256
    }
);

// Redirect the user to the connect URI with the returned ticket.
var redirectTo = $"{ticket.ConnectUri}?ticket={ticket.ConnectParams.Ticket}";
Console.WriteLine($"Ticket expires in {ticket.ExpiresIn}s");

// Persist ticket.AuthSession - it is required to complete the flow.
var authSession = ticket.AuthSession;
```

> **Note**
> Include `offline_access` in `Scopes` if you need the connected account to keep a refresh token. `CodeChallengeMethod` is required whenever `CodeChallenge` is supplied.

### 2. Complete the link flow

After the provider redirects back to your `RedirectUri` with a connect code, exchange it:

```csharp
using Auth0.MyAccountApi;

var connectedAccount = await client.ConnectedAccounts.CompleteAsync(
    new CompleteConnectedAccountsRequestContent
    {
        AuthSession = "<AUTH_SESSION>",
        ConnectCode = "<CONNECT_CODE_FROM_CALLBACK>",
        RedirectUri = "https://app.example.com/callback",
        CodeVerifier = "<PKCE_CODE_VERIFIER>"
    }
);

Console.WriteLine($"{connectedAccount.Connection} ({connectedAccount.AccessType})");
Console.WriteLine($"Scopes: {string.Join(", ", connectedAccount.Scopes ?? [])}");
```

> **Important**
> `RedirectUri` must exactly match the value used in step 1, and `CodeVerifier` must match the `CodeChallenge` sent there.

### Listing and removing connected accounts

```csharp
using Auth0.MyAccountApi;

var connectedAccounts = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters { Take = 20 }
);

await foreach (var account in connectedAccounts)
{
    Console.WriteLine($"{account.Id}: {account.Connection} (created {account.CreatedAt:u})");
}

await client.ConnectedAccounts.DeleteAsync("<CONNECTED_ACCOUNT_ID>");
```

### Filtering by connection

Filter by one or many connections using `ConnectionNamesFilter`:

```csharp
// Filter by a single connection...
var google = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters
    {
        Connection = ConnectionNamesFilter.FromString("google-oauth2")
    }
);

// ...or by several.
var social = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters
    {
        Connection = ConnectionNamesFilter.FromListOfString(["google-oauth2", "github"])
    }
);
```

### Discovering available connections

To find out which connections the user can link, list them first:

```csharp
using Auth0.MyAccountApi;
using Auth0.MyAccountApi.ConnectedAccounts;

var connections = await client.ConnectedAccounts.Connections.ListAsync(
    new ListConnectedAccountsConnectionsRequestParameters { Take = 20 }
);

await foreach (var connection in connections)
{
    Console.WriteLine($"{connection.Name} ({connection.Strategy})");
}
```

> **Note**
> `ListConnectedAccountsConnectionsRequestParameters` lives in the `Auth0.MyAccountApi.ConnectedAccounts` namespace, unlike the other request types which live in `Auth0.MyAccountApi`.

---

## Pagination

The connected-accounts list endpoints are cursor-paginated and return a `Pager<T>`, which implements `IAsyncEnumerable<T>`. The simplest approach is to `await foreach` over it - the SDK fetches subsequent pages transparently:

```csharp
using Auth0.MyAccountApi;

var connectedAccounts = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters { Take = 20 }
);

await foreach (var account in connectedAccounts)
{
    Console.WriteLine(account.Id);
}
```

To handle a page at a time, iterate `AsPagesAsync()`:

```csharp
var pager = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters { Take = 10 }
);

await foreach (var page in pager.AsPagesAsync())
{
    Console.WriteLine($"Page with {page.Items.Count} item(s)");
}
```

Or drive the cursor manually via `CurrentPage`, `HasNextPage`, and `GetNextPageAsync()`:

```csharp
var pager = await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters { Take = 10 }
);

foreach (var account in pager.CurrentPage.Items)
{
    Console.WriteLine(account.Id);
}

while (pager.HasNextPage)
{
    var next = await pager.GetNextPageAsync();
    foreach (var account in next.Items)
    {
        Console.WriteLine(account.Id);
    }
}
```

> **Note**
> `Take` accepts values from 1 to 20 and defaults to 10. `client.AuthenticationMethods.ListAsync` and `client.Factors.ListAsync` are not paginated - they return the full collection.

---

## Request Options

Options can be configured at the client level (affecting all requests) or per request. See the [configuration tables in the README](./README.md#configuration) for every available option.

```csharp
using Auth0.MyAccountApi;

// Client-level options (applied to every request).
var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    MaxRetries = 3,
    Timeout = TimeSpan.FromSeconds(10),
    AdditionalHeaders = new Dictionary<string, string?>
    {
        { "X-Custom-Header", "custom-value" }
    }
});

// Per-request overrides.
var method = await client.AuthenticationMethods.GetAsync(
    "<AUTHENTICATION_METHOD_ID>",
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

---

## Cancellation

Every operation accepts a `CancellationToken` as its final argument:

```csharp
using Auth0.MyAccountApi;

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

var methods = await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters(),
    options: null,
    cancellationToken: cts.Token
);
```

---

## Raw Responses

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

---

## Error Handling

When the API returns a non-success status code (4xx or 5xx), the SDK throws a typed subclass of `MyAccountApiException`. Each typed subclass exposes a strongly-typed `ErrorResponse` body, so validation failures can be surfaced field by field:

```csharp
using Auth0.MyAccountApi;

try
{
    var response = await client.AuthenticationMethods.CreateAsync(request);
}
catch (BadRequestError e)
{
    // Body is a strongly-typed ErrorResponse.
    Console.WriteLine($"{e.Body.Title}: {e.Body.Detail}");

    foreach (var validationError in e.Body.ValidationErrors ?? [])
    {
        Console.WriteLine($"  {validationError.Field}: {validationError.Detail}");
    }
}
catch (UnauthorizedError e)
{
    // The user's access token is missing, expired, or lacks the required scope.
    Console.WriteLine(e.Body.Detail);
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

All error types derive from `MyAccountApiException`, which in turn derives from `MyAccountException` - the base type for every exception this SDK raises. `ErrorResponse` exposes `Type`, `Status`, `Title`, `Detail`, and an optional `ValidationErrors` collection, where each `ValidationError` carries `Detail`, `Field`, `Pointer`, and `Source`. See the [error type table in the README](./README.md#error-handling) for the full list of typed exceptions.

---

## Explicit Null Values

By default, fields with `null` values are omitted from the request. Query parameters modelled as `Optional<T?>` let you distinguish "omit this parameter" from "send it explicitly as null":

```csharp
using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Core;

// Omit the filter entirely (default).
var all = new ListAuthenticationMethodsRequestParameters();

// Send an explicit value.
var filtered = new ListAuthenticationMethodsRequestParameters
{
    Type = FactorTypeEnum.Email
};

// Explicitly send a null value rather than omitting the parameter.
var explicitNull = new ListAuthenticationMethodsRequestParameters
{
    Type = Optional<FactorTypeEnum?>.Of(null)
};

// Opt out of the default Take = 10.
var noTake = new ListConnectedAccountsRequestParameters { Take = Optional<int?>.Of(null) };
```

Assigning a value directly also works - an implicit conversion wraps it for you.

---

## Forward Compatible Enums

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

---

## Retries

The SDK automatically retries requests with exponential backoff and jitter on the following status codes:

- `408` (Timeout)
- `429` (Too Many Requests)
- `5XX` (Server Errors)

The default retry limit is 2 attempts. The `Retry-After` and `X-RateLimit-Reset` response headers are respected when present, and the delay is capped at 60 seconds. Configure the limit via the `MaxRetries` option:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    MaxRetries = 5
});
```

You can also override `MaxRetries` per request via [`RequestOptions`](#request-options).

---

## Timeouts

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

You can also override `Timeout` per request via [`RequestOptions`](#request-options).

---

## Base URL

When `Domain` is set, the Base URL defaults to `https://{Domain}/me/v1`. Set `BaseUrl` directly to override this behaviour - for example, when targeting a custom domain or a local test server. When both are provided, `BaseUrl` takes precedence.

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    BaseUrl = "https://custom.example.com/me/v1",
    TokenProvider = tokenProvider
});
```

---

## HttpClient Lifetime

`MyAccountClient` implements `IDisposable`. If you do not supply an `HttpClient`, the client creates one and disposes it for you:

```csharp
using Auth0.MyAccountApi;

using var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider
});
```

If you supply your own `HttpClient` - the recommended approach in long-running applications, typically via `IHttpClientFactory` - you retain ownership and the SDK will not dispose it:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    HttpClient = httpClient // Your responsibility to dispose.
});
```

The `ITokenProvider` is never disposed by the client - the caller owns its lifetime.

---

## Telemetry

Every request carries an `Auth0-Client` header so Auth0 can understand SDK adoption and prioritise support. No configuration is required. The value is base64url-encoded JSON:

```json
{ "name": "MyAccount.NET", "version": "1.0.0", "env": { "target": "NET8.0" } }
```

It contains only the SDK name, the SDK version, and the .NET runtime target - never tokens, user identifiers, or request payloads.

The header is applied before `AdditionalHeaders`, so you can override it - client-wide:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    AdditionalHeaders = new Dictionary<string, string?>
    {
        { "Auth0-Client", myOwnAgentString }
    }
});
```

or for a single request via `RequestOptions.AdditionalHeaders`:

```csharp
await client.Factors.ListAsync(new RequestOptions
{
    AdditionalHeaders = new Dictionary<string, string?>
    {
        { "Auth0-Client", myOwnAgentString }
    }
});
```
