# Authentication Methods

Authentication methods are the factors the signed-in user can authenticate with - passwords, passkeys, TOTP authenticator apps, email and SMS one-time codes, push notifications, WebAuthn security keys, and recovery codes.

Two sub-clients are involved:

- `client.Factors` reports which factors the **tenant** has enabled.
- `client.AuthenticationMethods` lists, enrols, verifies, updates, and deletes the **user's** methods.

## Listing Available Factors

Before offering enrollment, check which factors the tenant has enabled:

```csharp
using Auth0.MyAccountApi;

var response = await client.Factors.ListAsync();

foreach (var factor in response.Factors)
{
    Console.WriteLine($"{factor.Type} - usage: {string.Join(", ", factor.Usage ?? [])}");
}
```

`Factor.Usage` reports whether the factor can be used as a `primary` or `secondary` authentication method. `Factors.ListAsync` takes no request parameters and is not paginated.

> [!NOTE]
> Requires the `read:me:factors` scope.

## Listing Authentication Methods

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

This endpoint is not paginated - it returns the full collection on `response.AuthenticationMethods`.

## Working With the AuthenticationMethod Union

Each `AuthenticationMethod` is a discriminated union over the supported factor types, because the useful properties differ per type: a phone method has a `PhoneNumber`, an email method has an `Email`, a TOTP method has neither.

Use `Visit` to handle every case exhaustively. The `onUnknown_` branch keeps your code working when Auth0 introduces new factor types:

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

Use `Match<T>` instead of `Visit` when each branch should produce a value:

```csharp
var labels = all.AuthenticationMethods.Select(method => method.Match(
    onPassword: _ => "Password",
    onPasskey: passkey => passkey.UserAgent ?? "Passkey",
    onRecoveryCode: _ => "Recovery code",
    onPushNotification: _ => "Push notification",
    onTotp: _ => "Authenticator app",
    onWebauthnPlatform: _ => "Device biometrics",
    onWebauthnRoaming: _ => "Security key",
    onPhone: phone => $"Phone {phone.PhoneNumber}",
    onEmail: email => $"Email {email.Email}",
    onUnknown_: (type, _) => $"Other ({type})"
));
```

If you only care about one or two types, use the `Is*` properties or the `TryAs*` methods:

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

| Member | Behaviour |
|---|---|
| `IsPhone`, `IsEmail`, ... | `true` when the union holds that variant |
| `AsPhone()`, `AsEmail()`, ... | Returns the variant, **throws** if it is a different type |
| `TryAsPhone(out var x)`, ... | Returns `bool`; sets `x` when the type matches |
| `Visit(...)` | Invokes one branch per variant, returns nothing |
| `Match<T>(...)` | Invokes one branch per variant, returns a value |

> [!NOTE]
> `As*` throws if the method is not of the requested type. Prefer `TryAs*` when the type is not already known, and always supply `onUnknown_` so a newly-introduced factor type does not throw.

## Enrolling Authentication Methods

Enrollment is always a two-step flow:

1. `CreateAsync` starts the enrollment and returns an `Id` plus an `AuthSession`.
2. `VerifyAsync` confirms it using the `AuthSession` and whatever proof the factor requires.

The `CreateAsync` response is itself a discriminated union - the variant depends on the factor being enrolled. The table below maps each factor to the response accessor and the verify payload to use:

| Factor | Create response accessor | Verify payload |
|---|---|---|
| Email | `AsMfaBaseCreationResponse()` | `VerifyEmailAuthenticationMethod` |
| Phone | `AsMfaBaseCreationResponse()` | `VerifyPhoneAuthenticationMethod` |
| Push notification | `AsMfaBaseCreationResponse()` | `VerifyPushNotificationAuthenticationMethod` |
| TOTP | `AsQrCodeCreationResponse()` | `VerifyTotpAuthenticationMethod` |
| Recovery code | `AsRecoveryCodeCreationResponse()` | `VerifyRecoveryCodeAuthenticationMethod` |
| Password | `AsPasswordCreationResponse()` | `VerifyPasswordAuthenticationMethod` |
| Passkey | `AsPasskeyCreationResponse()` | `VerifyPasskeyAuthenticationMethod` |

`CreateAuthenticationMethodRequestContent` models the seven factor types above. The create response can additionally surface `AsWebAuthnCreationResponse()`, verified with `VerifyWebAuthnPlatformAuthenticationMethod` or `VerifyWebAuthnRoamingAuthenticationMethod`; WebAuthn platform and roaming methods otherwise appear on list and get responses as `AsWebauthnPlatform()` / `AsWebauthnRoaming()`.

> [!NOTE]
> Enrollment requires the `create:me:authentication_methods` scope.

### Email

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

### Phone (SMS or voice)

Set `PreferredAuthenticationMethod` to choose between an SMS and a voice call:

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

### TOTP (authenticator app)

TOTP enrollment returns a QR code URI to display to the user, plus a manual-entry fallback:

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

### Recovery Code

Verification requires only the `AuthSession` - there is no code for the user to enter, because the code *is* the response:

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

> [!IMPORTANT]
> The recovery code is returned exactly once. Display or download it during this flow; it cannot be retrieved afterwards.

### Changing a Password

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

### Passkeys and WebAuthn

Passkey enrollment requires a browser round-trip: Auth0 returns WebAuthn creation options, the browser produces an attestation via `navigator.credentials.create()`, and you post that attestation back:

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

> [!NOTE]
> Unlike the other creation responses, `PasskeyCreationResponse` does not expose an `Id` property - it carries only `AuthSession` and `AuthnParamsPublicKey`. Read the authentication method ID from the `Location` header of the create response (see [Raw Responses](pagination-and-errors.md#raw-responses)).

## Managing Existing Authentication Methods

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

| Operation | Required scope |
|---|---|
| `GetAsync`, `ListAsync` | `read:me:authentication_methods` |
| `CreateAsync`, `VerifyAsync` | `create:me:authentication_methods` |
| `UpdateAsync` | `update:me:authentication_methods` |
| `DeleteAsync` | `delete:me:authentication_methods` |

> [!NOTE]
> The authentication method ID is returned in the `Location` header when creating a method, and as the `Id` property on list and create responses. Use it exactly as returned, without modification.

## Next Steps

- [Connected Accounts](connected-accounts.md) - Link external identity providers
- [Pagination & Error Handling](pagination-and-errors.md) - Handle typed errors and forward-compatible enums
- [Configuration](configuration.md) - Configure retries, timeouts, headers, and the base URL
