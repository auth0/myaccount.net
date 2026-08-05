# Change Log

## [1.0.0-beta.0](https://github.com/auth0/myaccount.net/tree/1.0.0-beta.0) (2026-08-05)

First beta release of the Auth0 My Account API SDK for .NET.

The My Account API operates in the context of the signed-in end user, so every call is made **on behalf of that user** using their access token — not a machine-to-machine token. The SDK lets an authenticated user manage their own account: enrolling and managing authentication methods (factors), and linking external identity providers as connected accounts.

**Added**

_Clients and authentication_

- `MyAccountClient` — recommended high-level entry point (`sealed`, `IDisposable`), wrapping the generated client with per-request token resolution via `ITokenProvider`. Constructs the Base URL as `https://{Domain}/me/v1` from `MyAccountClientOptions.Domain`; an explicitly set `BaseUrl` takes precedence and is used as-is. Only disposes the internal `HttpClient` when it created it — a caller-supplied `HttpClient` and the `ITokenProvider` are left alone
- `MyAccountApiClient` — low-level generated client accepting a static bearer token, for callers who manage the token and `HttpClient` themselves
- `ITokenProvider` abstraction for supplying the end-user access token from any source (session, secure store, cache, refresh-token exchange)
- `DelegateTokenProvider` — `ITokenProvider` backed by a cancellable async delegate, invoked on every request with no caching; throws `InvalidOperationException` if the delegate returns a null or empty token
- `MyAccountClientOptions` with `TokenProvider` (required), `Domain`, `BaseUrl`, `HttpClient`, `Timeout` (default 30 seconds), `MaxRetries` (default 2), and `AdditionalHeaders`
- Argument validation at client construction — `ArgumentNullException` for a missing `options` or `TokenProvider`, and `ArgumentException` when neither `Domain` nor `BaseUrl` is set, or when `Domain` includes a URL scheme, path, or trailing slash

_Factors API_

- `client.Factors.ListAsync(...)` — list the factors enabled for the tenant and available for enrollment by the authenticated user

_Authentication Methods API_

- `client.AuthenticationMethods.ListAsync(...)` — retrieve the authenticated user's authentication methods, optionally filtered by `FactorTypeEnum`
- `client.AuthenticationMethods.CreateAsync(...)` — start enrollment of a supported authentication method
- `client.AuthenticationMethods.VerifyAsync(...)` — confirm enrollment of a supported authentication method
- `client.AuthenticationMethods.GetAsync(...)`, `UpdateAsync(...)`, and `DeleteAsync(...)` — retrieve, update, and delete a single authentication method by ID
- Support for nine factor types across enrollment, verification, and retrieval: `password`, `passkey`, `webauthn-platform`, `webauthn-roaming`, `totp`, `phone`, `email`, `push-notification`, and `recovery-code`
- Discriminated-union response types with `Is*` predicates and `As*` accessors (`AuthenticationMethod`, `CreateAuthenticationMethodResponseContent`, `GetAuthenticationMethodResponseContent`, `UpdateAuthenticationMethodResponseContent`, `VerifyAuthenticationMethodResponseContent`) — e.g. `IsTotp` / `AsTotp()`, `IsQrCodeCreationResponse()` / `AsQrCodeCreationResponse()`
- Per-factor creation-response shapes: `MfaBaseCreationResponse`, `QrCodeCreationResponse`, `RecoveryCodeCreationResponse`, `PasswordCreationResponse`, `PasskeyCreationResponse`, and `WebAuthnCreationResponse`
- Per-factor verification payloads: `VerifyEmailAuthenticationMethod`, `VerifyPhoneAuthenticationMethod`, `VerifyTotpAuthenticationMethod`, `VerifyPushNotificationAuthenticationMethod`, `VerifyRecoveryCodeAuthenticationMethod`, `VerifyPasswordAuthenticationMethod`, `VerifyPasskeyAuthenticationMethod`, `VerifyWebAuthnPlatformAuthenticationMethod`, and `VerifyWebAuthnRoamingAuthenticationMethod`
- Password policy types surfaced on password enrollment: `PasswordAuthenticationMethodPolicy`, `PasswordComplexityPolicy`, `PasswordDictionaryPolicy`, `PasswordHistoryPolicy`, and `PasswordProfileDataPolicy`
- WebAuthn and passkey types: `PublicKeyCredentialCreationOptions`, `PublicKeyCredentialParameter`, `AuthenticatorSelection`, `AuthenticatorAttestationResponse`, and `RelyingParty`, with `AuthenticatorAttachmentEnum`, `UserVerificationEnum`, `ResidentKeyEnum`, and `CredentialDeviceTypeEnum`

_Connected Accounts API_

- `client.ConnectedAccounts.CreateAsync(...)` — start an authorization flow to link the user's account with an external identity provider; supports PKCE (`CodeChallenge`, `CodeChallengeMethod`), `Scopes`, `State`, and pass-through `AuthorizationParams` (`AcrValues`, `Audience`, `Resource`, `Display`, `IdTokenHint`, `LoginHint`, `MaxAge`, `Prompt`, `UiLocales`)
- `client.ConnectedAccounts.CompleteAsync(...)` — complete a previously started link flow
- `client.ConnectedAccounts.ListAsync(...)` — retrieve the user's connected accounts as a `Pager<ConnectedAccount>`
- `client.ConnectedAccounts.DeleteAsync(...)` — delete a connected account by ID
- `client.ConnectedAccounts.Connections.ListAsync(...)` — retrieve connections available for account linking as a `Pager<ConnectedAccountConnection>`
- `ConnectionNamesFilter` union to filter connected accounts by a single connection name (`FromString`) or several (`FromListOfString`)

_Core capabilities_

- Cursor-based pagination via `Pager<T>` and `BiPager<T>`, with `IAsyncEnumerable<TItem>` iteration and `AsPagesAsync(...)` for page-at-a-time access
- Automatic retry with exponential backoff and jitter on `408`, `429`, and `5XX` responses — honours `Retry-After` and `X-RateLimit-Reset` headers, and caps any single delay at 60 seconds
- `RequestOptions` for per-request overrides of `Timeout`, `MaxRetries`, `BaseUrl`, `HttpClient`, `AdditionalHeaders`, `AdditionalQueryParameters`, and `AdditionalBodyProperties`
- `.WithRawResponse()` on every operation to access the HTTP status code, URL, and response headers alongside the parsed data
- `Optional<T>` struct to distinguish "field not sent" from "field sent as null" on update requests
- Forward-compatible enums (`StringEnum<T>`, with `FromCustom(...)`) that preserve unknown values returned by the API rather than failing deserialization
- `AdditionalProperties` / `ReadOnlyAdditionalProperties` to round-trip JSON fields not yet modelled by the SDK
- Typed exceptions for HTTP error responses — `BadRequestError` (400), `UnauthorizedError` (401), `ForbiddenError` (403), `NotFoundError` (404), `UnsupportedMediaTypeError` (415), and `TooManyRequestsError` (429) — all deriving from `MyAccountApiException` (exposing `StatusCode`, `Body`, and `RawResponse`), which in turn derives from the `MyAccountException` base type
- Automatic `Auth0-Client` telemetry header (SDK name, version, and .NET runtime target; no secrets or PII) applied on every request path, including retries and pagination, and overridable via `AdditionalHeaders` [\#22](https://github.com/auth0/myaccount.net/pull/22) ([kailash-b](https://github.com/kailash-b))
- Multi-framework support: .NET Framework 4.6.2, .NET Standard 2.0, .NET 8.0, and .NET 9.0