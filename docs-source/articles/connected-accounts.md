# Connected Accounts

Connected accounts let the signed-in user link their Auth0 account to an external identity provider - Google, GitHub, Microsoft, and so on - so your application can act against that provider on the user's behalf.

Linking is a two-step, redirect-based flow:

1. `CreateAsync` returns a connect URI and a ticket. Redirect the user there.
2. The provider redirects back to your `RedirectUri` with a connect code, which `CompleteAsync` exchanges for the linked account.

> [!NOTE]
> Connected accounts are part of Token Vault. The scopes `create:me:connected_accounts`, `read:me:connected_accounts`, and `delete:me:connected_accounts` gate the operations below. There is no update operation - re-link the account to change its scopes.

## Discovering Available Connections

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

> [!IMPORTANT]
> `ListConnectedAccountsConnectionsRequestParameters` lives in the `Auth0.MyAccountApi.ConnectedAccounts` namespace, unlike the other request types which live in `Auth0.MyAccountApi`. Omitting that `using` produces a `CS0246` compile error.

Use `connection.Name` as the `Connection` value when starting the link flow.

## 1. Start the Link Flow

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

| Field | Purpose |
|---|---|
| `Connection` | Name of the connection to link, as returned by `Connections.ListAsync` |
| `RedirectUri` | Where the provider sends the user back; must match the value passed to `CompleteAsync` |
| `Scopes` | Scopes to request **at the external provider** - not Auth0 scopes |
| `State` | Opaque value echoed back to your callback; use it for CSRF protection |
| `CodeChallenge` / `CodeChallengeMethod` | PKCE challenge; `CodeChallengeMethod` is required whenever `CodeChallenge` is supplied |

> [!NOTE]
> Include `offline_access` in `Scopes` if you need the connected account to keep a refresh token. Without it, the resulting `AccessType` will not be `offline`.

Store `ticket.AuthSession` and your PKCE code verifier somewhere tied to the user's session - both are needed in step 2, and `ExpiresIn` bounds how long they stay useful.

## 2. Complete the Link Flow

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

> [!IMPORTANT]
> `RedirectUri` must exactly match the value used in step 1, and `CodeVerifier` must match the `CodeChallenge` sent there. A mismatch fails with a `BadRequestError`.

## Listing and Removing Connected Accounts

`ListAsync` returns a `Pager<ConnectedAccount>` - `await foreach` over it and the SDK fetches subsequent pages transparently:

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

See [Pagination](pagination-and-errors.md#pagination) for page-at-a-time and manual-cursor iteration.

## Filtering by Connection

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

`ConnectionNamesFilter` is a union over `string` and `IEnumerable<string>`, which is why the two factory methods exist rather than a single overloaded property.

## Operation Reference

| Operation | Returns | Required scope |
|---|---|---|
| `ConnectedAccounts.Connections.ListAsync` | `Pager<ConnectedAccountConnection>` | `read:me:connected_accounts` |
| `ConnectedAccounts.CreateAsync` | Connect ticket and URI | `create:me:connected_accounts` |
| `ConnectedAccounts.CompleteAsync` | `ConnectedAccount` | `create:me:connected_accounts` |
| `ConnectedAccounts.ListAsync` | `Pager<ConnectedAccount>` | `read:me:connected_accounts` |
| `ConnectedAccounts.DeleteAsync` | - | `delete:me:connected_accounts` |

## Next Steps

- [Pagination & Error Handling](pagination-and-errors.md) - Paging patterns and typed errors
- [Configuration](configuration.md) - Configure retries, timeouts, headers, and the base URL
- [Authentication Methods](authentication-methods.md) - Enrol and manage factors
