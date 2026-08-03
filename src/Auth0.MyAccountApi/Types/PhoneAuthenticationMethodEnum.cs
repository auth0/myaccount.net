using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(PhoneAuthenticationMethodEnum.PhoneAuthenticationMethodEnumSerializer))]
[Serializable]
public readonly record struct PhoneAuthenticationMethodEnum : IStringEnum
{
    public static readonly PhoneAuthenticationMethodEnum Sms = new(Values.Sms);

    public static readonly PhoneAuthenticationMethodEnum Voice = new(Values.Voice);

    public PhoneAuthenticationMethodEnum(string value)
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
    public static PhoneAuthenticationMethodEnum FromCustom(string value)
    {
        return new PhoneAuthenticationMethodEnum(value);
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

    public static bool operator ==(PhoneAuthenticationMethodEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PhoneAuthenticationMethodEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PhoneAuthenticationMethodEnum value) => value.Value;

    public static explicit operator PhoneAuthenticationMethodEnum(string value) => new(value);

    internal class PhoneAuthenticationMethodEnumSerializer
        : JsonConverter<PhoneAuthenticationMethodEnum>
    {
        public override PhoneAuthenticationMethodEnum Read(
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
            return new PhoneAuthenticationMethodEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PhoneAuthenticationMethodEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PhoneAuthenticationMethodEnum ReadAsPropertyName(
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
            return new PhoneAuthenticationMethodEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PhoneAuthenticationMethodEnum value,
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
        public const string Sms = "sms";

        public const string Voice = "voice";
    }
}
