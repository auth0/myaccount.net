// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Nodes;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Request content for creating an authentication method
/// </summary>
[JsonConverter(typeof(CreateAuthenticationMethodRequestContent.JsonConverter))]
[Serializable]
public record CreateAuthenticationMethodRequestContent
{
    internal CreateAuthenticationMethodRequestContent(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.Passkey"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Passkey value
    )
    {
        Type = "passkey";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.Totp"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Totp value
    )
    {
        Type = "totp";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.PushNotification"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.PushNotification value
    )
    {
        Type = "push-notification";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.RecoveryCode"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.RecoveryCode value
    )
    {
        Type = "recovery-code";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.Email"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Email value
    )
    {
        Type = "email";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.Phone"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Phone value
    )
    {
        Type = "phone";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CreateAuthenticationMethodRequestContent with <see cref="CreateAuthenticationMethodRequestContent.Password"/>.
    /// </summary>
    public CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Password value
    )
    {
        Type = "password";
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
    /// Returns true if <see cref="Type"/> is "passkey"
    /// </summary>
    public bool IsPasskey => Type == "passkey";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "totp"
    /// </summary>
    public bool IsTotp => Type == "totp";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "push-notification"
    /// </summary>
    public bool IsPushNotification => Type == "push-notification";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "recovery-code"
    /// </summary>
    public bool IsRecoveryCode => Type == "recovery-code";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "email"
    /// </summary>
    public bool IsEmail => Type == "email";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "phone"
    /// </summary>
    public bool IsPhone => Type == "phone";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "password"
    /// </summary>
    public bool IsPassword => Type == "password";

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod"/> if <see cref="Type"/> is 'passkey', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'passkey'.</exception>
    public Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod AsPasskey() =>
        IsPasskey
            ? (Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'passkey'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreateTotpAuthenticationMethod"/> if <see cref="Type"/> is 'totp', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'totp'.</exception>
    public Auth0.MyAccountApi.CreateTotpAuthenticationMethod AsTotp() =>
        IsTotp
            ? (Auth0.MyAccountApi.CreateTotpAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'totp'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod"/> if <see cref="Type"/> is 'push-notification', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'push-notification'.</exception>
    public Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod AsPushNotification() =>
        IsPushNotification
            ? (Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'push-notification'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod"/> if <see cref="Type"/> is 'recovery-code', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'recovery-code'.</exception>
    public Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod AsRecoveryCode() =>
        IsRecoveryCode
            ? (Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'recovery-code'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreateEmailAuthenticationMethod"/> if <see cref="Type"/> is 'email', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'email'.</exception>
    public Auth0.MyAccountApi.CreateEmailAuthenticationMethod AsEmail() =>
        IsEmail
            ? (Auth0.MyAccountApi.CreateEmailAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'email'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreatePhoneAuthenticationMethod"/> if <see cref="Type"/> is 'phone', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'phone'.</exception>
    public Auth0.MyAccountApi.CreatePhoneAuthenticationMethod AsPhone() =>
        IsPhone
            ? (Auth0.MyAccountApi.CreatePhoneAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'phone'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Auth0.MyAccountApi.CreatePasswordAuthenticationMethod"/> if <see cref="Type"/> is 'password', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'password'.</exception>
    public Auth0.MyAccountApi.CreatePasswordAuthenticationMethod AsPassword() =>
        IsPassword
            ? (Auth0.MyAccountApi.CreatePasswordAuthenticationMethod)Value!
            : throw new global::System.Exception(
                "CreateAuthenticationMethodRequestContent.Type is not 'password'"
            );

    public T Match<T>(
        Func<Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod, T> onPasskey,
        Func<Auth0.MyAccountApi.CreateTotpAuthenticationMethod, T> onTotp,
        Func<Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod, T> onPushNotification,
        Func<Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod, T> onRecoveryCode,
        Func<Auth0.MyAccountApi.CreateEmailAuthenticationMethod, T> onEmail,
        Func<Auth0.MyAccountApi.CreatePhoneAuthenticationMethod, T> onPhone,
        Func<Auth0.MyAccountApi.CreatePasswordAuthenticationMethod, T> onPassword,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "passkey" => onPasskey(AsPasskey()),
            "totp" => onTotp(AsTotp()),
            "push-notification" => onPushNotification(AsPushNotification()),
            "recovery-code" => onRecoveryCode(AsRecoveryCode()),
            "email" => onEmail(AsEmail()),
            "phone" => onPhone(AsPhone()),
            "password" => onPassword(AsPassword()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod> onPasskey,
        Action<Auth0.MyAccountApi.CreateTotpAuthenticationMethod> onTotp,
        Action<Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod> onPushNotification,
        Action<Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod> onRecoveryCode,
        Action<Auth0.MyAccountApi.CreateEmailAuthenticationMethod> onEmail,
        Action<Auth0.MyAccountApi.CreatePhoneAuthenticationMethod> onPhone,
        Action<Auth0.MyAccountApi.CreatePasswordAuthenticationMethod> onPassword,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "passkey":
                onPasskey(AsPasskey());
                break;
            case "totp":
                onTotp(AsTotp());
                break;
            case "push-notification":
                onPushNotification(AsPushNotification());
                break;
            case "recovery-code":
                onRecoveryCode(AsRecoveryCode());
                break;
            case "email":
                onEmail(AsEmail());
                break;
            case "phone":
                onPhone(AsPhone());
                break;
            case "password":
                onPassword(AsPassword());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsPasskey(out Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod? value)
    {
        if (Type == "passkey")
        {
            value = (Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreateTotpAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsTotp(out Auth0.MyAccountApi.CreateTotpAuthenticationMethod? value)
    {
        if (Type == "totp")
        {
            value = (Auth0.MyAccountApi.CreateTotpAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsPushNotification(
        out Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod? value
    )
    {
        if (Type == "push-notification")
        {
            value = (Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsRecoveryCode(
        out Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod? value
    )
    {
        if (Type == "recovery-code")
        {
            value = (Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreateEmailAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsEmail(out Auth0.MyAccountApi.CreateEmailAuthenticationMethod? value)
    {
        if (Type == "email")
        {
            value = (Auth0.MyAccountApi.CreateEmailAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreatePhoneAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsPhone(out Auth0.MyAccountApi.CreatePhoneAuthenticationMethod? value)
    {
        if (Type == "phone")
        {
            value = (Auth0.MyAccountApi.CreatePhoneAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Auth0.MyAccountApi.CreatePasswordAuthenticationMethod"/> and returns true if successful.
    /// </summary>
    public bool TryAsPassword(out Auth0.MyAccountApi.CreatePasswordAuthenticationMethod? value)
    {
        if (Type == "password")
        {
            value = (Auth0.MyAccountApi.CreatePasswordAuthenticationMethod)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Passkey value
    ) => new(value);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Totp value
    ) => new(value);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.PushNotification value
    ) => new(value);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.RecoveryCode value
    ) => new(value);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Email value
    ) => new(value);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Phone value
    ) => new(value);

    public static implicit operator CreateAuthenticationMethodRequestContent(
        CreateAuthenticationMethodRequestContent.Password value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<CreateAuthenticationMethodRequestContent>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(CreateAuthenticationMethodRequestContent).IsAssignableFrom(typeToConvert);

        public override CreateAuthenticationMethodRequestContent Read(
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
                "passkey" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod"
                        ),
                "totp" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreateTotpAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreateTotpAuthenticationMethod"
                        ),
                "push-notification" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod"
                        ),
                "recovery-code" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod"
                        ),
                "email" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreateEmailAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreateEmailAuthenticationMethod"
                        ),
                "phone" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreatePhoneAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreatePhoneAuthenticationMethod"
                        ),
                "password" =>
                    jsonWithoutDiscriminator.Deserialize<Auth0.MyAccountApi.CreatePasswordAuthenticationMethod?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Auth0.MyAccountApi.CreatePasswordAuthenticationMethod"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new CreateAuthenticationMethodRequestContent(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateAuthenticationMethodRequestContent value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "passkey" => JsonSerializer.SerializeToNode(value.Value, options),
                    "totp" => JsonSerializer.SerializeToNode(value.Value, options),
                    "push-notification" => JsonSerializer.SerializeToNode(value.Value, options),
                    "recovery-code" => JsonSerializer.SerializeToNode(value.Value, options),
                    "email" => JsonSerializer.SerializeToNode(value.Value, options),
                    "phone" => JsonSerializer.SerializeToNode(value.Value, options),
                    "password" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }

        public override CreateAuthenticationMethodRequestContent ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new JsonException("The JSON property name could not be read as a string.");
            return new CreateAuthenticationMethodRequestContent(stringValue, stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateAuthenticationMethodRequestContent value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Type);
        }
    }

    /// <summary>
    /// Discriminated union type for passkey
    /// </summary>
    [Serializable]
    public struct Passkey
    {
        public Passkey(Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.Passkey(
            Auth0.MyAccountApi.CreatePasskeyAuthenticationMethod value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for totp
    /// </summary>
    [Serializable]
    public struct Totp
    {
        public Totp(Auth0.MyAccountApi.CreateTotpAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreateTotpAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.Totp(
            Auth0.MyAccountApi.CreateTotpAuthenticationMethod value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for push-notification
    /// </summary>
    [Serializable]
    public struct PushNotification
    {
        public PushNotification(Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.PushNotification(
            Auth0.MyAccountApi.CreatePushNotificationAuthenticationMethod value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for recovery-code
    /// </summary>
    [Serializable]
    public struct RecoveryCode
    {
        public RecoveryCode(Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.RecoveryCode(
            Auth0.MyAccountApi.CreateRecoveryCodeAuthenticationMethod value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for email
    /// </summary>
    [Serializable]
    public struct Email
    {
        public Email(Auth0.MyAccountApi.CreateEmailAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreateEmailAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.Email(
            Auth0.MyAccountApi.CreateEmailAuthenticationMethod value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for phone
    /// </summary>
    [Serializable]
    public struct Phone
    {
        public Phone(Auth0.MyAccountApi.CreatePhoneAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreatePhoneAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.Phone(
            Auth0.MyAccountApi.CreatePhoneAuthenticationMethod value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for password
    /// </summary>
    [Serializable]
    public struct Password
    {
        public Password(Auth0.MyAccountApi.CreatePasswordAuthenticationMethod value)
        {
            Value = value;
        }

        internal Auth0.MyAccountApi.CreatePasswordAuthenticationMethod Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator CreateAuthenticationMethodRequestContent.Password(
            Auth0.MyAccountApi.CreatePasswordAuthenticationMethod value
        ) => new(value);
    }
}
