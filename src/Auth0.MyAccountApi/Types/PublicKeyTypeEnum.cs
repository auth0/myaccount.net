using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(PublicKeyTypeEnum.PublicKeyTypeEnumSerializer))]
[Serializable]
public readonly record struct PublicKeyTypeEnum : IStringEnum
{
    public static readonly PublicKeyTypeEnum PublicKey = new(Values.PublicKey);

    public PublicKeyTypeEnum(string value)
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
    public static PublicKeyTypeEnum FromCustom(string value)
    {
        return new PublicKeyTypeEnum(value);
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

    public static bool operator ==(PublicKeyTypeEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PublicKeyTypeEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PublicKeyTypeEnum value) => value.Value;

    public static explicit operator PublicKeyTypeEnum(string value) => new(value);

    internal class PublicKeyTypeEnumSerializer : JsonConverter<PublicKeyTypeEnum>
    {
        public override PublicKeyTypeEnum Read(
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
            return new PublicKeyTypeEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PublicKeyTypeEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PublicKeyTypeEnum ReadAsPropertyName(
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
            return new PublicKeyTypeEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PublicKeyTypeEnum value,
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
        public const string PublicKey = "public-key";
    }
}
