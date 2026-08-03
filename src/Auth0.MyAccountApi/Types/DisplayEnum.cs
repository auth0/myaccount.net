using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(DisplayEnum.DisplayEnumSerializer))]
[Serializable]
public readonly record struct DisplayEnum : IStringEnum
{
    public static readonly DisplayEnum Page = new(Values.Page);

    public static readonly DisplayEnum Popup = new(Values.Popup);

    public static readonly DisplayEnum Touch = new(Values.Touch);

    public static readonly DisplayEnum Wap = new(Values.Wap);

    public DisplayEnum(string value)
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
    public static DisplayEnum FromCustom(string value)
    {
        return new DisplayEnum(value);
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

    public static bool operator ==(DisplayEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DisplayEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DisplayEnum value) => value.Value;

    public static explicit operator DisplayEnum(string value) => new(value);

    internal class DisplayEnumSerializer : JsonConverter<DisplayEnum>
    {
        public override DisplayEnum Read(
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
            return new DisplayEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DisplayEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override DisplayEnum ReadAsPropertyName(
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
            return new DisplayEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            DisplayEnum value,
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
        public const string Page = "page";

        public const string Popup = "popup";

        public const string Touch = "touch";

        public const string Wap = "wap";
    }
}
