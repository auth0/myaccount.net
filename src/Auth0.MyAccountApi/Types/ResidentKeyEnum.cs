using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(ResidentKeyEnum.ResidentKeyEnumSerializer))]
[Serializable]
public readonly record struct ResidentKeyEnum : IStringEnum
{
    public static readonly ResidentKeyEnum Required = new(Values.Required);

    public ResidentKeyEnum(string value)
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
    public static ResidentKeyEnum FromCustom(string value)
    {
        return new ResidentKeyEnum(value);
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

    public static bool operator ==(ResidentKeyEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ResidentKeyEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ResidentKeyEnum value) => value.Value;

    public static explicit operator ResidentKeyEnum(string value) => new(value);

    internal class ResidentKeyEnumSerializer : JsonConverter<ResidentKeyEnum>
    {
        public override ResidentKeyEnum Read(
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
            return new ResidentKeyEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ResidentKeyEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ResidentKeyEnum ReadAsPropertyName(
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
            return new ResidentKeyEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ResidentKeyEnum value,
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
    }
}
