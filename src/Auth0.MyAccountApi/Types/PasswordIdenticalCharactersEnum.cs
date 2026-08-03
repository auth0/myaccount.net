using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(PasswordIdenticalCharactersEnum.PasswordIdenticalCharactersEnumSerializer))]
[Serializable]
public readonly record struct PasswordIdenticalCharactersEnum : IStringEnum
{
    public static readonly PasswordIdenticalCharactersEnum Allow = new(Values.Allow);

    public static readonly PasswordIdenticalCharactersEnum Block = new(Values.Block);

    public PasswordIdenticalCharactersEnum(string value)
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
    public static PasswordIdenticalCharactersEnum FromCustom(string value)
    {
        return new PasswordIdenticalCharactersEnum(value);
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

    public static bool operator ==(PasswordIdenticalCharactersEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PasswordIdenticalCharactersEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PasswordIdenticalCharactersEnum value) => value.Value;

    public static explicit operator PasswordIdenticalCharactersEnum(string value) => new(value);

    internal class PasswordIdenticalCharactersEnumSerializer
        : JsonConverter<PasswordIdenticalCharactersEnum>
    {
        public override PasswordIdenticalCharactersEnum Read(
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
            return new PasswordIdenticalCharactersEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PasswordIdenticalCharactersEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PasswordIdenticalCharactersEnum ReadAsPropertyName(
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
            return new PasswordIdenticalCharactersEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PasswordIdenticalCharactersEnum value,
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
        public const string Allow = "allow";

        public const string Block = "block";
    }
}
