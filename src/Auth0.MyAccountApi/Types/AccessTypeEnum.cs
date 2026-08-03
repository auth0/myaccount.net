using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(AccessTypeEnum.AccessTypeEnumSerializer))]
[Serializable]
public readonly record struct AccessTypeEnum : IStringEnum
{
    public static readonly AccessTypeEnum Offline = new(Values.Offline);

    public AccessTypeEnum(string value)
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
    public static AccessTypeEnum FromCustom(string value)
    {
        return new AccessTypeEnum(value);
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

    public static bool operator ==(AccessTypeEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AccessTypeEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AccessTypeEnum value) => value.Value;

    public static explicit operator AccessTypeEnum(string value) => new(value);

    internal class AccessTypeEnumSerializer : JsonConverter<AccessTypeEnum>
    {
        public override AccessTypeEnum Read(
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
            return new AccessTypeEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AccessTypeEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AccessTypeEnum ReadAsPropertyName(
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
            return new AccessTypeEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AccessTypeEnum value,
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
        public const string Offline = "offline";
    }
}
