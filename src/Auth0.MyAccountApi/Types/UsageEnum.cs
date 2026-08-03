using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(UsageEnum.UsageEnumSerializer))]
[Serializable]
public readonly record struct UsageEnum : IStringEnum
{
    public static readonly UsageEnum Primary = new(Values.Primary);

    public static readonly UsageEnum Secondary = new(Values.Secondary);

    public UsageEnum(string value)
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
    public static UsageEnum FromCustom(string value)
    {
        return new UsageEnum(value);
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

    public static bool operator ==(UsageEnum value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(UsageEnum value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(UsageEnum value) => value.Value;

    public static explicit operator UsageEnum(string value) => new(value);

    internal class UsageEnumSerializer : JsonConverter<UsageEnum>
    {
        public override UsageEnum Read(
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
            return new UsageEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            UsageEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override UsageEnum ReadAsPropertyName(
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
            return new UsageEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            UsageEnum value,
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
        public const string Primary = "primary";

        public const string Secondary = "secondary";
    }
}
