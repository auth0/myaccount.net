using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(UserVerificationEnum.UserVerificationEnumSerializer))]
[Serializable]
public readonly record struct UserVerificationEnum : IStringEnum
{
    public static readonly UserVerificationEnum Required = new(Values.Required);

    public static readonly UserVerificationEnum Preferred = new(Values.Preferred);

    public static readonly UserVerificationEnum Discouraged = new(Values.Discouraged);

    public UserVerificationEnum(string value)
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
    public static UserVerificationEnum FromCustom(string value)
    {
        return new UserVerificationEnum(value);
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

    public static bool operator ==(UserVerificationEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UserVerificationEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UserVerificationEnum value) => value.Value;

    public static explicit operator UserVerificationEnum(string value) => new(value);

    internal class UserVerificationEnumSerializer : JsonConverter<UserVerificationEnum>
    {
        public override UserVerificationEnum Read(
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
            return new UserVerificationEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            UserVerificationEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override UserVerificationEnum ReadAsPropertyName(
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
            return new UserVerificationEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            UserVerificationEnum value,
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
        public const string Required = "required";

        public const string Preferred = "preferred";

        public const string Discouraged = "discouraged";
    }
}
