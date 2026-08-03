using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(FactorTypeEnum.FactorTypeEnumSerializer))]
[Serializable]
public readonly record struct FactorTypeEnum : IStringEnum
{
    public static readonly FactorTypeEnum Password = new(Values.Password);

    public static readonly FactorTypeEnum Passkey = new(Values.Passkey);

    public static readonly FactorTypeEnum WebauthnPlatform = new(Values.WebauthnPlatform);

    public static readonly FactorTypeEnum WebauthnRoaming = new(Values.WebauthnRoaming);

    public static readonly FactorTypeEnum Totp = new(Values.Totp);

    public static readonly FactorTypeEnum Phone = new(Values.Phone);

    public static readonly FactorTypeEnum Email = new(Values.Email);

    public static readonly FactorTypeEnum PushNotification = new(Values.PushNotification);

    public static readonly FactorTypeEnum RecoveryCode = new(Values.RecoveryCode);

    public FactorTypeEnum(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static FactorTypeEnum FromCustom(string value)
    {
        return new FactorTypeEnum(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(FactorTypeEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FactorTypeEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FactorTypeEnum value) => value.Value;

    public static explicit operator FactorTypeEnum(string value) => new(value);

    internal class FactorTypeEnumSerializer : JsonConverter<FactorTypeEnum>
    {
        public override FactorTypeEnum Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new FactorTypeEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FactorTypeEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override FactorTypeEnum ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new FactorTypeEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            FactorTypeEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Password = "password";

        public const string Passkey = "passkey";

        public const string WebauthnPlatform = "webauthn-platform";

        public const string WebauthnRoaming = "webauthn-roaming";

        public const string Totp = "totp";

        public const string Phone = "phone";

        public const string Email = "email";

        public const string PushNotification = "push-notification";

        public const string RecoveryCode = "recovery-code";
    }
}
