// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(CreateAuthenticationMethodResponseContent.JsonConverter))]
[Serializable]
public class CreateAuthenticationMethodResponseContent
{
    private CreateAuthenticationMethodResponseContent(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Type discriminator
    /// </summary>
    [JsonIgnore]
    public string Type { get; internal set; }

    /// <summary>
    /// Union value
    /// </summary>
    [JsonIgnore]
    public object? Value { get; internal set; }

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.PasskeyCreationResponse value.
    /// </summary>
    public static CreateAuthenticationMethodResponseContent FromPasskeyCreationResponse(
        Auth0.MyAccountApi.PasskeyCreationResponse value
    ) => new("passkeyCreationResponse", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.WebAuthnCreationResponse value.
    /// </summary>
    public static CreateAuthenticationMethodResponseContent FromWebAuthnCreationResponse(
        Auth0.MyAccountApi.WebAuthnCreationResponse value
    ) => new("webAuthnCreationResponse", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.MfaBaseCreationResponse value.
    /// </summary>
    public static CreateAuthenticationMethodResponseContent FromMfaBaseCreationResponse(
        Auth0.MyAccountApi.MfaBaseCreationResponse value
    ) => new("mfaBaseCreationResponse", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.QrCodeCreationResponse value.
    /// </summary>
    public static CreateAuthenticationMethodResponseContent FromQrCodeCreationResponse(
        Auth0.MyAccountApi.QrCodeCreationResponse value
    ) => new("qrCodeCreationResponse", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.RecoveryCodeCreationResponse value.
    /// </summary>
    public static CreateAuthenticationMethodResponseContent FromRecoveryCodeCreationResponse(
        Auth0.MyAccountApi.RecoveryCodeCreationResponse value
    ) => new("recoveryCodeCreationResponse", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.PasswordCreationResponse value.
    /// </summary>
    public static CreateAuthenticationMethodResponseContent FromPasswordCreationResponse(
        Auth0.MyAccountApi.PasswordCreationResponse value
    ) => new("passwordCreationResponse", value);

    /// <summary>
    /// Returns true if <see cref="Type"/> is "passkeyCreationResponse"
    /// </summary>
    public bool IsPasskeyCreationResponse() => Type == "passkeyCreationResponse";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "webAuthnCreationResponse"
    /// </summary>
    public bool IsWebAuthnCreationResponse() => Type == "webAuthnCreationResponse";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "mfaBaseCreationResponse"
    /// </summary>
    public bool IsMfaBaseCreationResponse() => Type == "mfaBaseCreationResponse";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "qrCodeCreationResponse"
    /// </summary>
    public bool IsQrCodeCreationResponse() => Type == "qrCodeCreationResponse";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "recoveryCodeCreationResponse"
    /// </summary>
    public bool IsRecoveryCodeCreationResponse() => Type == "recoveryCodeCreationResponse";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "passwordCreationResponse"
    /// </summary>
    public bool IsPasswordCreationResponse() => Type == "passwordCreationResponse";

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.PasskeyCreationResponse"/> if <see cref="Type"/> is 'passkeyCreationResponse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'passkeyCreationResponse'.</exception>
    public Auth0.MyAccountApi.PasskeyCreationResponse AsPasskeyCreationResponse() =>
        IsPasskeyCreationResponse()
            ? (Auth0.MyAccountApi.PasskeyCreationResponse)Value!
            : throw new MyAccountException("Union type is not 'passkeyCreationResponse'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.WebAuthnCreationResponse"/> if <see cref="Type"/> is 'webAuthnCreationResponse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'webAuthnCreationResponse'.</exception>
    public Auth0.MyAccountApi.WebAuthnCreationResponse AsWebAuthnCreationResponse() =>
        IsWebAuthnCreationResponse()
            ? (Auth0.MyAccountApi.WebAuthnCreationResponse)Value!
            : throw new MyAccountException("Union type is not 'webAuthnCreationResponse'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.MfaBaseCreationResponse"/> if <see cref="Type"/> is 'mfaBaseCreationResponse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'mfaBaseCreationResponse'.</exception>
    public Auth0.MyAccountApi.MfaBaseCreationResponse AsMfaBaseCreationResponse() =>
        IsMfaBaseCreationResponse()
            ? (Auth0.MyAccountApi.MfaBaseCreationResponse)Value!
            : throw new MyAccountException("Union type is not 'mfaBaseCreationResponse'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.QrCodeCreationResponse"/> if <see cref="Type"/> is 'qrCodeCreationResponse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'qrCodeCreationResponse'.</exception>
    public Auth0.MyAccountApi.QrCodeCreationResponse AsQrCodeCreationResponse() =>
        IsQrCodeCreationResponse()
            ? (Auth0.MyAccountApi.QrCodeCreationResponse)Value!
            : throw new MyAccountException("Union type is not 'qrCodeCreationResponse'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.RecoveryCodeCreationResponse"/> if <see cref="Type"/> is 'recoveryCodeCreationResponse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'recoveryCodeCreationResponse'.</exception>
    public Auth0.MyAccountApi.RecoveryCodeCreationResponse AsRecoveryCodeCreationResponse() =>
        IsRecoveryCodeCreationResponse()
            ? (Auth0.MyAccountApi.RecoveryCodeCreationResponse)Value!
            : throw new MyAccountException("Union type is not 'recoveryCodeCreationResponse'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.PasswordCreationResponse"/> if <see cref="Type"/> is 'passwordCreationResponse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'passwordCreationResponse'.</exception>
    public Auth0.MyAccountApi.PasswordCreationResponse AsPasswordCreationResponse() =>
        IsPasswordCreationResponse()
            ? (Auth0.MyAccountApi.PasswordCreationResponse)Value!
            : throw new MyAccountException("Union type is not 'passwordCreationResponse'");

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.PasskeyCreationResponse"/> and returns true if successful.
    /// </summary>
    public bool TryGetPasskeyCreationResponse(out Auth0.MyAccountApi.PasskeyCreationResponse? value)
    {
        if (Type == "passkeyCreationResponse")
        {
            value = (Auth0.MyAccountApi.PasskeyCreationResponse)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.WebAuthnCreationResponse"/> and returns true if successful.
    /// </summary>
    public bool TryGetWebAuthnCreationResponse(
        out Auth0.MyAccountApi.WebAuthnCreationResponse? value
    )
    {
        if (Type == "webAuthnCreationResponse")
        {
            value = (Auth0.MyAccountApi.WebAuthnCreationResponse)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.MfaBaseCreationResponse"/> and returns true if successful.
    /// </summary>
    public bool TryGetMfaBaseCreationResponse(out Auth0.MyAccountApi.MfaBaseCreationResponse? value)
    {
        if (Type == "mfaBaseCreationResponse")
        {
            value = (Auth0.MyAccountApi.MfaBaseCreationResponse)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.QrCodeCreationResponse"/> and returns true if successful.
    /// </summary>
    public bool TryGetQrCodeCreationResponse(out Auth0.MyAccountApi.QrCodeCreationResponse? value)
    {
        if (Type == "qrCodeCreationResponse")
        {
            value = (Auth0.MyAccountApi.QrCodeCreationResponse)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.RecoveryCodeCreationResponse"/> and returns true if successful.
    /// </summary>
    public bool TryGetRecoveryCodeCreationResponse(
        out Auth0.MyAccountApi.RecoveryCodeCreationResponse? value
    )
    {
        if (Type == "recoveryCodeCreationResponse")
        {
            value = (Auth0.MyAccountApi.RecoveryCodeCreationResponse)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.PasswordCreationResponse"/> and returns true if successful.
    /// </summary>
    public bool TryGetPasswordCreationResponse(
        out Auth0.MyAccountApi.PasswordCreationResponse? value
    )
    {
        if (Type == "passwordCreationResponse")
        {
            value = (Auth0.MyAccountApi.PasswordCreationResponse)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public T Match<T>(
        Func<Auth0.MyAccountApi.PasskeyCreationResponse, T> onPasskeyCreationResponse,
        Func<Auth0.MyAccountApi.WebAuthnCreationResponse, T> onWebAuthnCreationResponse,
        Func<Auth0.MyAccountApi.MfaBaseCreationResponse, T> onMfaBaseCreationResponse,
        Func<Auth0.MyAccountApi.QrCodeCreationResponse, T> onQrCodeCreationResponse,
        Func<Auth0.MyAccountApi.RecoveryCodeCreationResponse, T> onRecoveryCodeCreationResponse,
        Func<Auth0.MyAccountApi.PasswordCreationResponse, T> onPasswordCreationResponse
    )
    {
        return Type switch
        {
            "passkeyCreationResponse" => onPasskeyCreationResponse(AsPasskeyCreationResponse()),
            "webAuthnCreationResponse" => onWebAuthnCreationResponse(AsWebAuthnCreationResponse()),
            "mfaBaseCreationResponse" => onMfaBaseCreationResponse(AsMfaBaseCreationResponse()),
            "qrCodeCreationResponse" => onQrCodeCreationResponse(AsQrCodeCreationResponse()),
            "recoveryCodeCreationResponse" => onRecoveryCodeCreationResponse(
                AsRecoveryCodeCreationResponse()
            ),
            "passwordCreationResponse" => onPasswordCreationResponse(AsPasswordCreationResponse()),
            _ => throw new MyAccountException($"Unknown union type: {Type}"),
        };
    }

    public void Visit(
        Action<Auth0.MyAccountApi.PasskeyCreationResponse> onPasskeyCreationResponse,
        Action<Auth0.MyAccountApi.WebAuthnCreationResponse> onWebAuthnCreationResponse,
        Action<Auth0.MyAccountApi.MfaBaseCreationResponse> onMfaBaseCreationResponse,
        Action<Auth0.MyAccountApi.QrCodeCreationResponse> onQrCodeCreationResponse,
        Action<Auth0.MyAccountApi.RecoveryCodeCreationResponse> onRecoveryCodeCreationResponse,
        Action<Auth0.MyAccountApi.PasswordCreationResponse> onPasswordCreationResponse
    )
    {
        switch (Type)
        {
            case "passkeyCreationResponse":
                onPasskeyCreationResponse(AsPasskeyCreationResponse());
                break;
            case "webAuthnCreationResponse":
                onWebAuthnCreationResponse(AsWebAuthnCreationResponse());
                break;
            case "mfaBaseCreationResponse":
                onMfaBaseCreationResponse(AsMfaBaseCreationResponse());
                break;
            case "qrCodeCreationResponse":
                onQrCodeCreationResponse(AsQrCodeCreationResponse());
                break;
            case "recoveryCodeCreationResponse":
                onRecoveryCodeCreationResponse(AsRecoveryCodeCreationResponse());
                break;
            case "passwordCreationResponse":
                onPasswordCreationResponse(AsPasswordCreationResponse());
                break;
            default:
                throw new MyAccountException($"Unknown union type: {Type}");
        }
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Type.GetHashCode();
            if (Value != null)
            {
                hashCode = (hashCode * 397) ^ Value.GetHashCode();
            }
            return hashCode;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is not CreateAuthenticationMethodResponseContent other)
            return false;

        // Compare type discriminators
        if (Type != other.Type)
            return false;

        // Compare values using EqualityComparer for deep comparison
        return System.Collections.Generic.EqualityComparer<object?>.Default.Equals(
            Value,
            other.Value
        );
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator CreateAuthenticationMethodResponseContent(
        Auth0.MyAccountApi.PasskeyCreationResponse value
    ) => new("passkeyCreationResponse", value);

    public static implicit operator CreateAuthenticationMethodResponseContent(
        Auth0.MyAccountApi.WebAuthnCreationResponse value
    ) => new("webAuthnCreationResponse", value);

    public static implicit operator CreateAuthenticationMethodResponseContent(
        Auth0.MyAccountApi.MfaBaseCreationResponse value
    ) => new("mfaBaseCreationResponse", value);

    public static implicit operator CreateAuthenticationMethodResponseContent(
        Auth0.MyAccountApi.QrCodeCreationResponse value
    ) => new("qrCodeCreationResponse", value);

    public static implicit operator CreateAuthenticationMethodResponseContent(
        Auth0.MyAccountApi.RecoveryCodeCreationResponse value
    ) => new("recoveryCodeCreationResponse", value);

    public static implicit operator CreateAuthenticationMethodResponseContent(
        Auth0.MyAccountApi.PasswordCreationResponse value
    ) => new("passwordCreationResponse", value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<CreateAuthenticationMethodResponseContent>
    {
        public override CreateAuthenticationMethodResponseContent? Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var document = JsonDocument.ParseValue(ref reader);

                var types = new (string Key, System.Type Type)[]
                {
                    ("passkeyCreationResponse", typeof(Auth0.MyAccountApi.PasskeyCreationResponse)),
                    (
                        "webAuthnCreationResponse",
                        typeof(Auth0.MyAccountApi.WebAuthnCreationResponse)
                    ),
                    ("mfaBaseCreationResponse", typeof(Auth0.MyAccountApi.MfaBaseCreationResponse)),
                    ("qrCodeCreationResponse", typeof(Auth0.MyAccountApi.QrCodeCreationResponse)),
                    (
                        "recoveryCodeCreationResponse",
                        typeof(Auth0.MyAccountApi.RecoveryCodeCreationResponse)
                    ),
                    (
                        "passwordCreationResponse",
                        typeof(Auth0.MyAccountApi.PasswordCreationResponse)
                    ),
                };

                foreach (var (key, type) in types)
                {
                    try
                    {
                        var value = document.Deserialize(type, options);
                        if (value != null)
                        {
                            CreateAuthenticationMethodResponseContent result = new(key, value);
                            return result;
                        }
                    }
                    catch (JsonException)
                    {
                        // Try next type;
                    }
                }
            }

            throw new JsonException(
                $"Cannot deserialize JSON token {reader.TokenType} into CreateAuthenticationMethodResponseContent"
            );
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateAuthenticationMethodResponseContent value,
            JsonSerializerOptions options
        )
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            value.Visit(
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options)
            );
        }

        public override CreateAuthenticationMethodResponseContent ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue = reader.GetString()!;
            CreateAuthenticationMethodResponseContent result = new("string", stringValue);
            return result;
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateAuthenticationMethodResponseContent value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value?.ToString() ?? "null");
        }
    }
}
