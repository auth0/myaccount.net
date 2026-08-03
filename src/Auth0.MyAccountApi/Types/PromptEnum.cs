using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(PromptEnum.PromptEnumSerializer))]
[Serializable]
public readonly record struct PromptEnum : IStringEnum
{
    public static readonly PromptEnum None = new(Values.None);

    public static readonly PromptEnum Login = new(Values.Login);

    public static readonly PromptEnum Consent = new(Values.Consent);

    public static readonly PromptEnum SelectAccount = new(Values.SelectAccount);

    public PromptEnum(string value)
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
    public static PromptEnum FromCustom(string value)
    {
        return new PromptEnum(value);
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

    public static bool operator ==(PromptEnum value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(PromptEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PromptEnum value) => value.Value;

    public static explicit operator PromptEnum(string value) => new(value);

    internal class PromptEnumSerializer : JsonConverter<PromptEnum>
    {
        public override PromptEnum Read(
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
            return new PromptEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PromptEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PromptEnum ReadAsPropertyName(
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
            return new PromptEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PromptEnum value,
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
        public const string None = "none";

        public const string Login = "login";

        public const string Consent = "consent";

        public const string SelectAccount = "select_account";
    }
}
