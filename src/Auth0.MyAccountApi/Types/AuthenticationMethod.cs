// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Nodes;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(AuthenticationMethod.JsonConverter))]
[Serializable]
public record AuthenticationMethod
{
    internal AuthenticationMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.Password"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.Password value)
    {
        Type = "password";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.Passkey"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.Passkey value)
    {
        Type = "passkey";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.RecoveryCode"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.RecoveryCode value)
    {
        Type = "recovery-code";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.PushNotification"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.PushNotification value)
    {
        Type = "push-notification";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.Totp"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.Totp value)
    {
        Type = "totp";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.WebauthnPlatform"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.WebauthnPlatform value)
    {
        Type = "webauthn-platform";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.WebauthnRoaming"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.WebauthnRoaming value)
    {
        Type = "webauthn-roaming";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.Phone"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.Phone value)
    {
        Type = "phone";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of AuthenticationMethod with <see cref="AuthenticationMethod.Email"/>.
    /// </summary>
    public AuthenticationMethod(AuthenticationMethod.Email value)
    {
        Type = "email";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Type"/> is "password"
    /// </summary>
    public bool IsPassword => Type == "password";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "passkey"
    /// </summary>
    public bool IsPasskey => Type == "passkey";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "recovery-code"
    /// </summary>
    public bool IsRecoveryCode => Type == "recovery-code";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "push-notification"
    /// </summary>
    public bool IsPushNotification => Type == "push-notification";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "totp"
    /// </summary>
    public bool IsTotp => Type == "totp";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "webauthn-platform"
    /// </summary>
    public bool IsWebauthnPlatform => Type == "webauthn-platform";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "webauthn-roaming"
    /// </summary>
    public bool IsWebauthnRoaming => Type == "webauthn-roaming";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "phone"
    /// </summary>
    public bool IsPhone => Type == "phone";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "email"
    /// </summary>
    public bool IsEmail => Type == "email";

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodPassword"/> if <see cref="Type"/> is 'password', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'password'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodPassword AsPassword() =>
        IsPassword
            ? (Auth0.MyAccountApi.AuthenticationMethodPassword)Value!
            : throw new global::System.Exception("AuthenticationMethod.Type is not 'password'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodPasskey"/> if <see cref="Type"/> is 'passkey', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'passkey'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodPasskey AsPasskey() =>
        IsPasskey
            ? (Auth0.MyAccountApi.AuthenticationMethodPasskey)Value!
            : throw new global::System.Exception("AuthenticationMethod.Type is not 'passkey'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode"/> if <see cref="Type"/> is 'recovery-code', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'recovery-code'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode AsRecoveryCode() =>
        IsRecoveryCode
            ? (Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode)Value!
            : throw new global::System.Exception(
                "AuthenticationMethod.Type is not 'recovery-code'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification"/> if <see cref="Type"/> is 'push-notification', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'push-notification'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification AsPushNotification() =>
        IsPushNotification
            ? (Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification)Value!
            : throw new global::System.Exception(
                "AuthenticationMethod.Type is not 'push-notification'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodMfaTotp"/> if <see cref="Type"/> is 'totp', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'totp'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodMfaTotp AsTotp() =>
        IsTotp
            ? (Auth0.MyAccountApi.AuthenticationMethodMfaTotp)Value!
            : throw new global::System.Exception("AuthenticationMethod.Type is not 'totp'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform"/> if <see cref="Type"/> is 'webauthn-platform', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'webauthn-platform'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform AsWebauthnPlatform() =>
        IsWebauthnPlatform
            ? (Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform)Value!
            : throw new global::System.Exception(
                "AuthenticationMethod.Type is not 'webauthn-platform'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming"/> if <see cref="Type"/> is 'webauthn-roaming', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'webauthn-roaming'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming AsWebauthnRoaming() =>
        IsWebauthnRoaming
            ? (Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming)Value!
            : throw new global::System.Exception(
                "AuthenticationMethod.Type is not 'webauthn-roaming'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodPhone"/> if <see cref="Type"/> is 'phone', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'phone'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodPhone AsPhone() =>
        IsPhone
            ? (Auth0.MyAccountApi.AuthenticationMethodPhone)Value!
            : throw new global::System.Exception("AuthenticationMethod.Type is not 'phone'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.AuthenticationMethodEmail"/> if <see cref="Type"/> is 'email', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'email'.</exception>
    public Auth0.MyAccountApi.AuthenticationMethodEmail AsEmail() =>
        IsEmail
            ? (Auth0.MyAccountApi.AuthenticationMethodEmail)Value!
            : throw new global::System.Exception("AuthenticationMethod.Type is not 'email'");

    public T Match<T>(
        Func<Auth0.MyAccountApi.AuthenticationMethodPassword, T> onPassword,
        Func<Auth0.MyAccountApi.AuthenticationMethodPasskey, T> onPasskey,
        Func<Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode, T> onRecoveryCode,
        Func<Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification, T> onPushNotification,
        Func<Auth0.MyAccountApi.AuthenticationMethodMfaTotp, T> onTotp,
        Func<Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform, T> onWebauthnPlatform,
        Func<Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming, T> onWebauthnRoaming,
        Func<Auth0.MyAccountApi.AuthenticationMethodPhone, T> onPhone,
        Func<Auth0.MyAccountApi.AuthenticationMethodEmail, T> onEmail,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "password" => onPassword(AsPassword()),
            "passkey" => onPasskey(AsPasskey()),
            "recovery-code" => onRecoveryCode(AsRecoveryCode()),
            "push-notification" => onPushNotification(AsPushNotification()),
            "totp" => onTotp(AsTotp()),
            "webauthn-platform" => onWebauthnPlatform(AsWebauthnPlatform()),
            "webauthn-roaming" => onWebauthnRoaming(AsWebauthnRoaming()),
            "phone" => onPhone(AsPhone()),
            "email" => onEmail(AsEmail()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Auth0.MyAccountApi.AuthenticationMethodPassword> onPassword,
        Action<Auth0.MyAccountApi.AuthenticationMethodPasskey> onPasskey,
        Action<Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode> onRecoveryCode,
        Action<Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification> onPushNotification,
        Action<Auth0.MyAccountApi.AuthenticationMethodMfaTotp> onTotp,
        Action<Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform> onWebauthnPlatform,
        Action<Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming> onWebauthnRoaming,
        Action<Auth0.MyAccountApi.AuthenticationMethodPhone> onPhone,
        Action<Auth0.MyAccountApi.AuthenticationMethodEmail> onEmail,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "password":
                onPassword(AsPassword());
                break;
            case "passkey":
                onPasskey(AsPasskey());
                break;
            case "recovery-code":
                onRecoveryCode(AsRecoveryCode());
                break;
            case "push-notification":
                onPushNotification(AsPushNotification());
                break;
            case "totp":
                onTotp(AsTotp());
                break;
            case "webauthn-platform":
                onWebauthnPlatform(AsWebauthnPlatform());
                break;
            case "webauthn-roaming":
                onWebauthnRoaming(AsWebauthnRoaming());
                break;
            case "phone":
                onPhone(AsPhone());
                break;
            case "email":
                onEmail(AsEmail());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodPassword"/> and returns true if successful.
    /// </summary>
    public bool TryAsPassword(out Auth0.MyAccountApi.AuthenticationMethodPassword? value)
    {
        if (Type == "password")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodPassword)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodPasskey"/> and returns true if successful.
    /// </summary>
    public bool TryAsPasskey(out Auth0.MyAccountApi.AuthenticationMethodPasskey? value)
    {
        if (Type == "passkey")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodPasskey)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode"/> and returns true if successful.
    /// </summary>
    public bool TryAsRecoveryCode(out Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode? value)
    {
        if (Type == "recovery-code")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification"/> and returns true if successful.
    /// </summary>
    public bool TryAsPushNotification(
        out Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification? value
    )
    {
        if (Type == "push-notification")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodMfaTotp"/> and returns true if successful.
    /// </summary>
    public bool TryAsTotp(out Auth0.MyAccountApi.AuthenticationMethodMfaTotp? value)
    {
        if (Type == "totp")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodMfaTotp)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform"/> and returns true if successful.
    /// </summary>
    public bool TryAsWebauthnPlatform(
        out Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform? value
    )
    {
        if (Type == "webauthn-platform")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming"/> and returns true if successful.
    /// </summary>
    public bool TryAsWebauthnRoaming(
        out Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming? value
    )
    {
        if (Type == "webauthn-roaming")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodPhone"/> and returns true if successful.
    /// </summary>
    public bool TryAsPhone(out Auth0.MyAccountApi.AuthenticationMethodPhone? value)
    {
        if (Type == "phone")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodPhone)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.AuthenticationMethodEmail"/> and returns true if successful.
    /// </summary>
    public bool TryAsEmail(out Auth0.MyAccountApi.AuthenticationMethodEmail? value)
    {
        if (Type == "email")
        {
            value = (Auth0.MyAccountApi.AuthenticationMethodEmail)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator AuthenticationMethod(AuthenticationMethod.Password value) =>
        new(value);

    public static implicit operator AuthenticationMethod(AuthenticationMethod.Passkey value) =>
        new(value);

    public static implicit operator AuthenticationMethod(AuthenticationMethod.RecoveryCode value) =>
        new(value);

    public static implicit operator AuthenticationMethod(
        AuthenticationMethod.PushNotification value
    ) => new(value);

    public static implicit operator AuthenticationMethod(AuthenticationMethod.Totp value) =>
        new(value);

    public static implicit operator AuthenticationMethod(
        AuthenticationMethod.WebauthnPlatform value
    ) => new(value);

    public static implicit operator AuthenticationMethod(
        AuthenticationMethod.WebauthnRoaming value
    ) => new(value);

    public static implicit operator AuthenticationMethod(AuthenticationMethod.Phone value) =>
        new(value);

    public static implicit operator AuthenticationMethod(AuthenticationMethod.Email value) =>
        new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<AuthenticationMethod>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(AuthenticationMethod).IsAssignableFrom(typeToConvert);

        public override AuthenticationMethod Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'type' is null");

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("type");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "password" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodPassword?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodPassword"
                        ),
                "passkey" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodPasskey?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodPasskey"
                        ),
                "recovery-code" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode"
                        ),
                "push-notification" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification"
                        ),
                "totp" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodMfaTotp?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodMfaTotp"
                        ),
                "webauthn-platform" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform"
                        ),
                "webauthn-roaming" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming"
                        ),
                "phone" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodPhone?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodPhone"
                        ),
                "email" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.AuthenticationMethodEmail?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.AuthenticationMethodEmail"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new AuthenticationMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AuthenticationMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "password" => JsonSerializer.SerializeToNode(value.Value, options),
                    "passkey" => JsonSerializer.SerializeToNode(value.Value, options),
                    "recovery-code" => JsonSerializer.SerializeToNode(value.Value, options),
                    "push-notification" => JsonSerializer.SerializeToNode(value.Value, options),
                    "totp" => JsonSerializer.SerializeToNode(value.Value, options),
                    "webauthn-platform" => JsonSerializer.SerializeToNode(value.Value, options),
                    "webauthn-roaming" => JsonSerializer.SerializeToNode(value.Value, options),
                    "phone" => JsonSerializer.SerializeToNode(value.Value, options),
                    "email" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }

        public override AuthenticationMethod ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new JsonException("The JSON property name could not be read as a string.");
            return new AuthenticationMethod(stringValue, stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AuthenticationMethod value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Type);
        }
    }

    /// <summary>
    /// Discriminated union type for password
    /// </summary>
    [Serializable]
    public struct Password
    {
        public Password(Auth0.MyAccountApi.AuthenticationMethodPassword value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodPassword Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.Password(
            Auth0.MyAccountApi.AuthenticationMethodPassword value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for passkey
    /// </summary>
    [Serializable]
    public struct Passkey
    {
        public Passkey(Auth0.MyAccountApi.AuthenticationMethodPasskey value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodPasskey Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.Passkey(
            Auth0.MyAccountApi.AuthenticationMethodPasskey value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for recovery-code
    /// </summary>
    [Serializable]
    public struct RecoveryCode
    {
        public RecoveryCode(Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.RecoveryCode(
            Auth0.MyAccountApi.AuthenticationMethodMfaRecoveryCode value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for push-notification
    /// </summary>
    [Serializable]
    public struct PushNotification
    {
        public PushNotification(Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.PushNotification(
            Auth0.MyAccountApi.AuthenticationMethodMfaPushNotification value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for totp
    /// </summary>
    [Serializable]
    public struct Totp
    {
        public Totp(Auth0.MyAccountApi.AuthenticationMethodMfaTotp value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodMfaTotp Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.Totp(
            Auth0.MyAccountApi.AuthenticationMethodMfaTotp value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for webauthn-platform
    /// </summary>
    [Serializable]
    public struct WebauthnPlatform
    {
        public WebauthnPlatform(Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.WebauthnPlatform(
            Auth0.MyAccountApi.AuthenticationMethodWebAuthnPlatform value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for webauthn-roaming
    /// </summary>
    [Serializable]
    public struct WebauthnRoaming
    {
        public WebauthnRoaming(Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.WebauthnRoaming(
            Auth0.MyAccountApi.AuthenticationMethodWebAuthnRoaming value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for phone
    /// </summary>
    [Serializable]
    public struct Phone
    {
        public Phone(Auth0.MyAccountApi.AuthenticationMethodPhone value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodPhone Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.Phone(
            Auth0.MyAccountApi.AuthenticationMethodPhone value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for email
    /// </summary>
    [Serializable]
    public struct Email
    {
        public Email(Auth0.MyAccountApi.AuthenticationMethodEmail value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.AuthenticationMethodEmail Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator AuthenticationMethod.Email(
            Auth0.MyAccountApi.AuthenticationMethodEmail value
        ) => new(value);
    }
}
