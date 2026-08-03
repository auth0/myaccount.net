// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Request content for verifying an authentication method
/// </summary>
[JsonConverter(typeof(VerifyAuthenticationMethodRequestContent.JsonConverter))]
[Serializable]
public class VerifyAuthenticationMethodRequestContent
{
    private VerifyAuthenticationMethodRequestContent(string type, object? value)
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
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyPasskeyAuthenticationMethod(
        Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod value
    ) => new("verifyPasskeyAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyPasswordAuthenticationMethod(
        Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod value
    ) => new("verifyPasswordAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyWebAuthnPlatformAuthenticationMethod(
        Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod value
    ) => new("verifyWebAuthnPlatformAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyWebAuthnRoamingAuthenticationMethod(
        Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod value
    ) => new("verifyWebAuthnRoamingAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyPushNotificationAuthenticationMethod(
        Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod value
    ) => new("verifyPushNotificationAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyRecoveryCodeAuthenticationMethod(
        Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod value
    ) => new("verifyRecoveryCodeAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyEmailAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyEmailAuthenticationMethod(
        Auth0.MyAccountApi.VerifyEmailAuthenticationMethod value
    ) => new("verifyEmailAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyPhoneAuthenticationMethod(
        Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod value
    ) => new("verifyPhoneAuthenticationMethod", value);

    /// <summary>
    /// Factory method to create a union from a Auth0.MyAccountApi.VerifyTotpAuthenticationMethod value.
    /// </summary>
    public static VerifyAuthenticationMethodRequestContent FromVerifyTotpAuthenticationMethod(
        Auth0.MyAccountApi.VerifyTotpAuthenticationMethod value
    ) => new("verifyTotpAuthenticationMethod", value);

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyPasskeyAuthenticationMethod"
    /// </summary>
    public bool IsVerifyPasskeyAuthenticationMethod() =>
        Type == "verifyPasskeyAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyPasswordAuthenticationMethod"
    /// </summary>
    public bool IsVerifyPasswordAuthenticationMethod() =>
        Type == "verifyPasswordAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyWebAuthnPlatformAuthenticationMethod"
    /// </summary>
    public bool IsVerifyWebAuthnPlatformAuthenticationMethod() =>
        Type == "verifyWebAuthnPlatformAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyWebAuthnRoamingAuthenticationMethod"
    /// </summary>
    public bool IsVerifyWebAuthnRoamingAuthenticationMethod() =>
        Type == "verifyWebAuthnRoamingAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyPushNotificationAuthenticationMethod"
    /// </summary>
    public bool IsVerifyPushNotificationAuthenticationMethod() =>
        Type == "verifyPushNotificationAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyRecoveryCodeAuthenticationMethod"
    /// </summary>
    public bool IsVerifyRecoveryCodeAuthenticationMethod() =>
        Type == "verifyRecoveryCodeAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyEmailAuthenticationMethod"
    /// </summary>
    public bool IsVerifyEmailAuthenticationMethod() => Type == "verifyEmailAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyPhoneAuthenticationMethod"
    /// </summary>
    public bool IsVerifyPhoneAuthenticationMethod() => Type == "verifyPhoneAuthenticationMethod";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "verifyTotpAuthenticationMethod"
    /// </summary>
    public bool IsVerifyTotpAuthenticationMethod() => Type == "verifyTotpAuthenticationMethod";

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod"/> if <see cref="Type"/> is 'verifyPasskeyAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyPasskeyAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod AsVerifyPasskeyAuthenticationMethod() =>
        IsVerifyPasskeyAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod)Value!
            : throw new MyAccountException("Union type is not 'verifyPasskeyAuthenticationMethod'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod"/> if <see cref="Type"/> is 'verifyPasswordAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyPasswordAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod AsVerifyPasswordAuthenticationMethod() =>
        IsVerifyPasswordAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod)Value!
            : throw new MyAccountException(
                "Union type is not 'verifyPasswordAuthenticationMethod'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod"/> if <see cref="Type"/> is 'verifyWebAuthnPlatformAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyWebAuthnPlatformAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod AsVerifyWebAuthnPlatformAuthenticationMethod() =>
        IsVerifyWebAuthnPlatformAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod)Value!
            : throw new MyAccountException(
                "Union type is not 'verifyWebAuthnPlatformAuthenticationMethod'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod"/> if <see cref="Type"/> is 'verifyWebAuthnRoamingAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyWebAuthnRoamingAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod AsVerifyWebAuthnRoamingAuthenticationMethod() =>
        IsVerifyWebAuthnRoamingAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod)Value!
            : throw new MyAccountException(
                "Union type is not 'verifyWebAuthnRoamingAuthenticationMethod'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod"/> if <see cref="Type"/> is 'verifyPushNotificationAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyPushNotificationAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod AsVerifyPushNotificationAuthenticationMethod() =>
        IsVerifyPushNotificationAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod)Value!
            : throw new MyAccountException(
                "Union type is not 'verifyPushNotificationAuthenticationMethod'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod"/> if <see cref="Type"/> is 'verifyRecoveryCodeAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyRecoveryCodeAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod AsVerifyRecoveryCodeAuthenticationMethod() =>
        IsVerifyRecoveryCodeAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod)Value!
            : throw new MyAccountException(
                "Union type is not 'verifyRecoveryCodeAuthenticationMethod'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyEmailAuthenticationMethod"/> if <see cref="Type"/> is 'verifyEmailAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyEmailAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyEmailAuthenticationMethod AsVerifyEmailAuthenticationMethod() =>
        IsVerifyEmailAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyEmailAuthenticationMethod)Value!
            : throw new MyAccountException("Union type is not 'verifyEmailAuthenticationMethod'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod"/> if <see cref="Type"/> is 'verifyPhoneAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyPhoneAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod AsVerifyPhoneAuthenticationMethod() =>
        IsVerifyPhoneAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod)Value!
            : throw new MyAccountException("Union type is not 'verifyPhoneAuthenticationMethod'");

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.VerifyTotpAuthenticationMethod"/> if <see cref="Type"/> is 'verifyTotpAuthenticationMethod', otherwise throws an exception.
    /// </summary>
    /// <exception cref="MyAccountException">Thrown when <see cref="Type"/> is not 'verifyTotpAuthenticationMethod'.</exception>
    public Auth0.MyAccountApi.VerifyTotpAuthenticationMethod AsVerifyTotpAuthenticationMethod() =>
        IsVerifyTotpAuthenticationMethod()
            ? (Auth0.MyAccountApi.VerifyTotpAuthenticationMethod)Value!
            : throw new MyAccountException("Union type is not 'verifyTotpAuthenticationMethod'");

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyPasskeyAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod? value
    )
    {
        if (Type == "verifyPasskeyAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyPasswordAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod? value
    )
    {
        if (Type == "verifyPasswordAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyWebAuthnPlatformAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod? value
    )
    {
        if (Type == "verifyWebAuthnPlatformAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyWebAuthnRoamingAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod? value
    )
    {
        if (Type == "verifyWebAuthnRoamingAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyPushNotificationAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod? value
    )
    {
        if (Type == "verifyPushNotificationAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyRecoveryCodeAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod? value
    )
    {
        if (Type == "verifyRecoveryCodeAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyEmailAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyEmailAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyEmailAuthenticationMethod? value
    )
    {
        if (Type == "verifyEmailAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyEmailAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyPhoneAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod? value
    )
    {
        if (Type == "verifyPhoneAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.VerifyTotpAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryGetVerifyTotpAuthenticationMethod(
        out Auth0.MyAccountApi.VerifyTotpAuthenticationMethod? value
    )
    {
        if (Type == "verifyTotpAuthenticationMethod")
        {
            value = (Auth0.MyAccountApi.VerifyTotpAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public T Match<T>(
        Func<
            Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod,
            T
        > onVerifyPasskeyAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod,
            T
        > onVerifyPasswordAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod,
            T
        > onVerifyWebAuthnPlatformAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod,
            T
        > onVerifyWebAuthnRoamingAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod,
            T
        > onVerifyPushNotificationAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod,
            T
        > onVerifyRecoveryCodeAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyEmailAuthenticationMethod,
            T
        > onVerifyEmailAuthenticationMethod,
        Func<
            Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod,
            T
        > onVerifyPhoneAuthenticationMethod,
        Func<Auth0.MyAccountApi.VerifyTotpAuthenticationMethod, T> onVerifyTotpAuthenticationMethod
    )
    {
        return Type switch
        {
            "verifyPasskeyAuthenticationMethod" => onVerifyPasskeyAuthenticationMethod(
                AsVerifyPasskeyAuthenticationMethod()
            ),
            "verifyPasswordAuthenticationMethod" => onVerifyPasswordAuthenticationMethod(
                AsVerifyPasswordAuthenticationMethod()
            ),
            "verifyWebAuthnPlatformAuthenticationMethod" =>
                onVerifyWebAuthnPlatformAuthenticationMethod(
                    AsVerifyWebAuthnPlatformAuthenticationMethod()
                ),
            "verifyWebAuthnRoamingAuthenticationMethod" =>
                onVerifyWebAuthnRoamingAuthenticationMethod(
                    AsVerifyWebAuthnRoamingAuthenticationMethod()
                ),
            "verifyPushNotificationAuthenticationMethod" =>
                onVerifyPushNotificationAuthenticationMethod(
                    AsVerifyPushNotificationAuthenticationMethod()
                ),
            "verifyRecoveryCodeAuthenticationMethod" => onVerifyRecoveryCodeAuthenticationMethod(
                AsVerifyRecoveryCodeAuthenticationMethod()
            ),
            "verifyEmailAuthenticationMethod" => onVerifyEmailAuthenticationMethod(
                AsVerifyEmailAuthenticationMethod()
            ),
            "verifyPhoneAuthenticationMethod" => onVerifyPhoneAuthenticationMethod(
                AsVerifyPhoneAuthenticationMethod()
            ),
            "verifyTotpAuthenticationMethod" => onVerifyTotpAuthenticationMethod(
                AsVerifyTotpAuthenticationMethod()
            ),
            _ => throw new MyAccountException($"Unknown union type: {Type}"),
        };
    }

    public void Visit(
        Action<Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod> onVerifyPasskeyAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod> onVerifyPasswordAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod> onVerifyWebAuthnPlatformAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod> onVerifyWebAuthnRoamingAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod> onVerifyPushNotificationAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod> onVerifyRecoveryCodeAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyEmailAuthenticationMethod> onVerifyEmailAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod> onVerifyPhoneAuthenticationMethod,
        Action<Auth0.MyAccountApi.VerifyTotpAuthenticationMethod> onVerifyTotpAuthenticationMethod
    )
    {
        switch (Type)
        {
            case "verifyPasskeyAuthenticationMethod":
                onVerifyPasskeyAuthenticationMethod(AsVerifyPasskeyAuthenticationMethod());
                break;
            case "verifyPasswordAuthenticationMethod":
                onVerifyPasswordAuthenticationMethod(AsVerifyPasswordAuthenticationMethod());
                break;
            case "verifyWebAuthnPlatformAuthenticationMethod":
                onVerifyWebAuthnPlatformAuthenticationMethod(
                    AsVerifyWebAuthnPlatformAuthenticationMethod()
                );
                break;
            case "verifyWebAuthnRoamingAuthenticationMethod":
                onVerifyWebAuthnRoamingAuthenticationMethod(
                    AsVerifyWebAuthnRoamingAuthenticationMethod()
                );
                break;
            case "verifyPushNotificationAuthenticationMethod":
                onVerifyPushNotificationAuthenticationMethod(
                    AsVerifyPushNotificationAuthenticationMethod()
                );
                break;
            case "verifyRecoveryCodeAuthenticationMethod":
                onVerifyRecoveryCodeAuthenticationMethod(
                    AsVerifyRecoveryCodeAuthenticationMethod()
                );
                break;
            case "verifyEmailAuthenticationMethod":
                onVerifyEmailAuthenticationMethod(AsVerifyEmailAuthenticationMethod());
                break;
            case "verifyPhoneAuthenticationMethod":
                onVerifyPhoneAuthenticationMethod(AsVerifyPhoneAuthenticationMethod());
                break;
            case "verifyTotpAuthenticationMethod":
                onVerifyTotpAuthenticationMethod(AsVerifyTotpAuthenticationMethod());
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
        if (obj is not VerifyAuthenticationMethodRequestContent other)
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

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod value
    ) => new("verifyPasskeyAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod value
    ) => new("verifyPasswordAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod value
    ) => new("verifyWebAuthnPlatformAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod value
    ) => new("verifyWebAuthnRoamingAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod value
    ) => new("verifyPushNotificationAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod value
    ) => new("verifyRecoveryCodeAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyEmailAuthenticationMethod value
    ) => new("verifyEmailAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod value
    ) => new("verifyPhoneAuthenticationMethod", value);

    public static implicit operator VerifyAuthenticationMethodRequestContent(
        Auth0.MyAccountApi.VerifyTotpAuthenticationMethod value
    ) => new("verifyTotpAuthenticationMethod", value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<VerifyAuthenticationMethodRequestContent>
    {
        public override VerifyAuthenticationMethodRequestContent? Read(
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
                    (
                        "verifyPasskeyAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyPasskeyAuthenticationMethod)
                    ),
                    (
                        "verifyPasswordAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyPasswordAuthenticationMethod)
                    ),
                    (
                        "verifyWebAuthnPlatformAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyWebAuthnPlatformAuthenticationMethod)
                    ),
                    (
                        "verifyWebAuthnRoamingAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyWebAuthnRoamingAuthenticationMethod)
                    ),
                    (
                        "verifyPushNotificationAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyPushNotificationAuthenticationMethod)
                    ),
                    (
                        "verifyRecoveryCodeAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyRecoveryCodeAuthenticationMethod)
                    ),
                    (
                        "verifyEmailAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyEmailAuthenticationMethod)
                    ),
                    (
                        "verifyPhoneAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyPhoneAuthenticationMethod)
                    ),
                    (
                        "verifyTotpAuthenticationMethod",
                        typeof(Auth0.MyAccountApi.VerifyTotpAuthenticationMethod)
                    ),
                };

                foreach (var (key, type) in types)
                {
                    try
                    {
                        var value = document.Deserialize(type, options);
                        if (value != null)
                        {
                            VerifyAuthenticationMethodRequestContent result = new(key, value);
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
                $"Cannot deserialize JSON token {reader.TokenType} into VerifyAuthenticationMethodRequestContent"
            );
        }

        public override void Write(
            Utf8JsonWriter writer,
            VerifyAuthenticationMethodRequestContent value,
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
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options),
                obj => JsonSerializer.Serialize(writer, obj, options)
            );
        }

        public override VerifyAuthenticationMethodRequestContent ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue = reader.GetString()!;
            VerifyAuthenticationMethodRequestContent result = new("string", stringValue);
            return result;
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            VerifyAuthenticationMethodRequestContent value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value?.ToString() ?? "null");
        }
    }
}
