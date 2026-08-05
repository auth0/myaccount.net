# Configuration

Options can be configured at the client level, where they affect every request, or per request, where they override the client-level value for that call only.

## Client Options

`MyAccountClientOptions` is passed to the `MyAccountClient` constructor:

| Option | Type | Default | Description |
|---|---|---|---|
| `TokenProvider` | `ITokenProvider` | **required** | Supplies the end user's access token on every request |
| `Domain` | `string?` | - | Auth0 tenant domain (e.g. `"your-tenant.auth0.com"`); used to construct the base URL as `https://{Domain}/me/v1` |
| `BaseUrl` | `string?` | - | Override the base URL directly; takes precedence over `Domain` |
| `HttpClient` | `HttpClient?` | SDK-created | Provide a custom `HttpClient` |
| `AdditionalHeaders` | `IDictionary<string, string?>` | empty | Additional HTTP headers sent with every request |
| `MaxRetries` | `int?` | `2` | Maximum retry attempts |
| `Timeout` | `TimeSpan?` | 30 seconds | Request timeout |

Either `Domain` or `BaseUrl` must be set, and `TokenProvider` is required. Omitting them throws at construction time rather than on first request.

## Per-Request Options

Every operation accepts an optional `RequestOptions` as its second-to-last argument:

| Option | Description |
|---|---|
| `MaxRetries` | Maximum retry attempts for this request |
| `Timeout` | Request timeout |
| `AdditionalHeaders` | Additional HTTP headers to send |
| `AdditionalQueryParameters` | Additional query parameters to append |
| `AdditionalBodyProperties` | Additional JSON body properties |
| `BaseUrl` | Override the base URL for this request |
| `HttpClient` | Override the `HttpClient` for this request |

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

`AdditionalQueryParameters` and `AdditionalBodyProperties` are escape hatches for API fields the SDK does not yet model - useful when the API ships a parameter ahead of an SDK release.

## Retries

The SDK automatically retries requests with exponential backoff and jitter on the following status codes:

- `408` (Timeout)
- `429` (Too Many Requests)
- `5XX` (Server Errors)

The default retry limit is 2 attempts. Configure it via `MaxRetries`:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    MaxRetries = 5
});
```

Delays are chosen in this order:

1. The `Retry-After` response header, if present (either a number of seconds or an HTTP date).
2. The `X-RateLimit-Reset` response header, if present (a Unix timestamp in seconds).
3. Exponential backoff from a 1 second base, doubling per attempt.

Whichever applies, the delay is capped at 60 seconds and jittered to avoid synchronized retries across instances. Set `MaxRetries = 0` to disable retries entirely - for example when your own middleware already handles them.

## Timeouts

The SDK defaults to a 30 second timeout. Configure it via `Timeout`:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    Domain = "<YOUR_AUTH0_DOMAIN>",
    TokenProvider = tokenProvider,
    Timeout = TimeSpan.FromSeconds(10)
});
```

The timeout applies per attempt, so a request that exhausts its retries can take longer than `Timeout` in total. Budget accordingly when both values are tuned upward.

## Base URL

When `Domain` is set, the base URL defaults to `https://{Domain}/me/v1`. Set `BaseUrl` directly to override this - for example when targeting a custom domain or a local test server. When both are provided, `BaseUrl` takes precedence:

```csharp
using Auth0.MyAccountApi;

var client = new MyAccountClient(new MyAccountClientOptions
{
    BaseUrl = "https://custom.example.com/me/v1",
    TokenProvider = tokenProvider
});
```

> [!NOTE]
> `Domain` must not include a scheme prefix or a path/trailing slash - use `"your-tenant.auth0.com"`, not `"https://your-tenant.auth0.com"` or `"your-tenant.auth0.com/"`. An `ArgumentException` is thrown if an invalid domain value is detected. When you set `BaseUrl` yourself, include the full `/me/v1` path.

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

The token is also passed through to `ITokenProvider.GetTokenAsync`, so a cancelled request cancels the token lookup too. In ASP.NET Core, pass the request's `HttpContext.RequestAborted` so abandoned client connections stop the outbound call.

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

The `ITokenProvider` is never disposed by the client - the caller owns its lifetime. See [ASP.NET Core Integration](authentication.md#aspnet-core-integration) for the recommended singleton registration.

## Next Steps

- [Pagination & Error Handling](pagination-and-errors.md) - Paging, raw responses, and typed errors
- [Authentication](authentication.md) - Token providers and ASP.NET Core integration
- [API Reference](../api/Auth0.MyAccountApi.yml) - Complete API documentation
