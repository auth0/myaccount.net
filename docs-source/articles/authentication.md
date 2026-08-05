# Authentication

Every My Account API call is made **on behalf of a signed-in end user**, authorized with that user's access token. The SDK never obtains a token itself - you supply it through an `ITokenProvider`.

> [!IMPORTANT]
> This is not a machine-to-machine API. A client-credentials token will not work; the token must be an end-user access token issued for the audience `https://{yourDomain}/me/` with the scopes matching the operations you intend to call.

## How Token Resolution Works

`MyAccountClient` calls `ITokenProvider.GetTokenAsync` on **every** request and sets the resulting value as the `Authorization: Bearer` header. Nothing is cached by the SDK, which has two consequences:

- You remain in full control of how the token is obtained, cached, and refreshed.
- A single `MyAccountClient` can be registered as a singleton and still act on behalf of whichever user is making the current request.

The `ITokenProvider` interface is a single method:

```csharp
using Auth0.MyAccountApi;

public interface ITokenProvider
{
    Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
}
```

## Custom Token Source (recommended)

`DelegateTokenProvider` adapts any `Func<CancellationToken, Task<string>>` into an `ITokenProvider`. Use it to read the token from an in-memory session, a secure store, a cache, or a refresh-token exchange:

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

> [!NOTE]
> The delegate must return a non-empty token. `DelegateTokenProvider` throws an `InvalidOperationException` if it returns null or whitespace, so a misconfigured token source fails loudly rather than sending an empty `Authorization` header.

## Implementing ITokenProvider

Implement `ITokenProvider` directly when you need token-lifecycle logic beyond a delegate - for example, caching a token until shortly before it expires and refreshing it in the background:

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

Pass the instance in as you would a `DelegateTokenProvider`:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = new MyTokenProvider()
});
```

> [!NOTE]
> The client never disposes the `ITokenProvider`, even if it implements `IDisposable`. The caller owns its lifetime. See [HttpClient Lifetime](configuration.md#httpclient-lifetime).

## Static Token

If you already hold a bearer token, the recommended approach is still `MyAccountClient` with a `DelegateTokenProvider`. This keeps token handling consistent and gives you access to all wrapper features such as retries and timeouts:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("<USER_ACCESS_TOKEN>"))
});
```

### Using MyAccountApiClient directly

`MyAccountApiClient` is the underlying generated client. It accepts a static token with no token-lifecycle management - the token will not be refreshed automatically, and you must construct the base URL yourself:

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

This is only recommended for advanced scenarios such as test harnesses, middleware, or environments that inject tokens externally. Prefer `MyAccountClient` everywhere else.

## ASP.NET Core Integration

Because the token provider is resolved per request, register `MyAccountClient` once as a singleton and read the current user's token off the active HTTP request:

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

Two details make this safe as a singleton:

- The delegate resolves `HttpContext` at call time, so each request uses its own user's token.
- Supplying an `HttpClient` from `IHttpClientFactory` avoids socket exhaustion and stale DNS in long-running applications.

## Troubleshooting

| Symptom | Likely cause |
|---|---|
| `InvalidOperationException` from the token provider | The delegate returned null or whitespace |
| `ArgumentException` at construction | `Domain` includes a scheme or trailing slash, or neither `Domain` nor `BaseUrl` was supplied |
| `UnauthorizedError` (401) | Token missing, expired, or issued for the wrong audience |
| `ForbiddenError` (403) | Token is valid but lacks the required scope for the operation |

See [Error Handling](pagination-and-errors.md#error-handling) for handling these in code, and the [Scopes table](getting-started.md#scopes) for which scope each operation needs.

## Next Steps

- [Authentication Methods](authentication-methods.md) - Enrol and manage factors
- [Connected Accounts](connected-accounts.md) - Link external identity providers
- [Configuration](configuration.md) - Configure retries, timeouts, headers, and the base URL
