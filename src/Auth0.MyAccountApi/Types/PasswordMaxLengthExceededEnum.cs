using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(PasswordMaxLengthExceededEnum.PasswordMaxLengthExceededEnumSerializer))]
[Serializable]
public readonly record struct PasswordMaxLengthExceededEnum : IStringEnum
{
    public static readonly PasswordMaxLengthExceededEnum Truncate = new(Values.Truncate);

    public static readonly PasswordMaxLengthExceededEnum Error = new(Values.Error);

    public PasswordMaxLengthExceededEnum(string value)
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
    public static PasswordMaxLengthExceededEnum FromCustom(string value)
    {
        return new PasswordMaxLengthExceededEnum(value);
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

    public static bool operator ==(PasswordMaxLengthExceededEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PasswordMaxLengthExceededEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PasswordMaxLengthExceededEnum value) => value.Value;

    public static explicit operator PasswordMaxLengthExceededEnum(string value) => new(value);

    internal class PasswordMaxLengthExceededEnumSerializer
        : JsonConverter<PasswordMaxLengthExceededEnum>
    {
        public override PasswordMaxLengthExceededEnum Read(
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
            return new PasswordMaxLengthExceededEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PasswordMaxLengthExceededEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PasswordMaxLengthExceededEnum ReadAsPropertyName(
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
            return new PasswordMaxLengthExceededEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PasswordMaxLengthExceededEnum value,
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
        public const string Truncate = "truncate";

        public const string Error = "error";
    }
}
