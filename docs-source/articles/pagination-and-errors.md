# Pagination & Error Handling

This page covers how to iterate paginated list endpoints, reach the raw HTTP response, handle typed errors, send explicit null values, and work with forward-compatible enums.

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

To handle a page at a time - for batch processing, or to render one page per screen - iterate `AsPagesAsync()`:

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

| Member | Description |
|---|---|
| `await foreach (var item in pager)` | Iterate every item across every page |
| `pager.AsPagesAsync()` | Iterate a page at a time |
| `pager.CurrentPage` | The page already fetched by the initial call |
| `pager.HasNextPage` | `true` when the API returned a next-page cursor |
| `pager.GetNextPageAsync()` | Fetch and return the next page |

> [!NOTE]
> `Take` accepts values from 1 to 20 and defaults to 10. `client.AuthenticationMethods.ListAsync` and `client.Factors.ListAsync` are **not** paginated - they return the full collection directly, not a `Pager<T>`.

## Raw Responses

Use `.WithRawResponse()` to access the status code, URL, and headers alongside the parsed response data:

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

`RawResponse` exposes `StatusCode` (`HttpStatusCode`), `Url` (`Uri`), and `Headers` (`ResponseHeaders`). This is how you read the `Location` header after creating an authentication method - see [Passkeys and WebAuthn](authentication-methods.md#passkeys-and-webauthn).

Every operation returns a `WithRawResponseTask<T>`, which awaits to the parsed data by default. Chaining `.WithRawResponse()` before awaiting is what opts you into the wrapper - there is no separate method to call.

## Error Handling

When the API returns a non-success status code (4xx or 5xx), the SDK throws a typed subclass of `MyAccountApiException`. Each subclass exposes a strongly-typed `ErrorResponse` body, so validation failures can be surfaced field by field:

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

### Typed exceptions

| Type | Status Code | Description |
|---|---|---|
| `BadRequestError` | 400 | Invalid request |
| `UnauthorizedError` | 401 | Token missing, invalid, or expired |
| `ForbiddenError` | 403 | Insufficient scope |
| `NotFoundError` | 404 | Resource not found |
| `UnsupportedMediaTypeError` | 415 | Unsupported request content type |
| `TooManyRequestsError` | 429 | Rate limit exceeded |

All of the above derive from `MyAccountApiException`, which in turn derives from `MyAccountException` - the base type for every exception this SDK raises. Catch `MyAccountApiException` for any API error, and `MyAccountException` to also cover client-side failures such as timeouts and serialization errors.

### The ErrorResponse body

| Property | Type | Description |
|---|---|---|
| `Type` | `string` | URI identifying the error type |
| `Status` | `double` | HTTP status code, repeated in the body |
| `Title` | `string` | Short, human-readable summary |
| `Detail` | `string` | Explanation specific to this occurrence |
| `ValidationErrors` | `IEnumerable<ValidationError>?` | Present on validation failures |

Each `ValidationError` carries `Detail`, plus optional `Field`, `Pointer`, and `Source` locating the offending input - enough to attach messages to individual form fields.

> [!NOTE]
> A `ForbiddenError` (403) almost always means the access token lacks a required scope rather than that the operation is unavailable. Check the [Scopes table](getting-started.md#scopes) against the operation you called.

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

Assigning a value directly also works - an implicit conversion wraps it for you, so `Optional<T?>` only needs to appear in your code when you want the explicit-null behaviour.

## Forward Compatible Enums

This SDK uses forward-compatible enums that handle unknown values gracefully, so new server-side values won't break your code:

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

Switch on `.Value` against the `.Values` string constants and always include a `default` branch. A value the SDK does not recognize deserializes successfully and round-trips unchanged rather than throwing.

The same principle applies to the response unions: `AuthenticationMethod.Visit` and `Match` require an `onUnknown_` branch for exactly this reason. See [Working With the AuthenticationMethod Union](authentication-methods.md#working-with-the-authenticationmethod-union).

## Next Steps

- [Configuration](configuration.md) - Retries, timeouts, and the base URL
- [Authentication Methods](authentication-methods.md) - Enrol and manage factors
- [API Reference](../api/Auth0.MyAccountApi.yml) - Complete API documentation
