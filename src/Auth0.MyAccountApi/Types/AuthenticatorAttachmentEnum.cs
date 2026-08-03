using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(AuthenticatorAttachmentEnum.AuthenticatorAttachmentEnumSerializer))]
[Serializable]
public readonly record struct AuthenticatorAttachmentEnum : IStringEnum
{
    public static readonly AuthenticatorAttachmentEnum Platform = new(Values.Platform);

    public static readonly AuthenticatorAttachmentEnum CrossPlatform = new(Values.CrossPlatform);

    public AuthenticatorAttachmentEnum(string value)
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
    public static AuthenticatorAttachmentEnum FromCustom(string value)
    {
        return new AuthenticatorAttachmentEnum(value);
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

    public static bool operator ==(AuthenticatorAttachmentEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AuthenticatorAttachmentEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AuthenticatorAttachmentEnum value) => value.Value;

    public static explicit operator AuthenticatorAttachmentEnum(string value) => new(value);

    internal class AuthenticatorAttachmentEnumSerializer
        : JsonConverter<AuthenticatorAttachmentEnum>
    {
        public override AuthenticatorAttachmentEnum Read(
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
            return new AuthenticatorAttachmentEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AuthenticatorAttachmentEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AuthenticatorAttachmentEnum ReadAsPropertyName(
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
            return new AuthenticatorAttachmentEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AuthenticatorAttachmentEnum value,
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
        public const string Platform = "platform";

        public const string CrossPlatform = "cross-platform";
    }
}
