using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(typeof(PasswordCharacterTypeRuleEnum.PasswordCharacterTypeRuleEnumSerializer))]
[Serializable]
public readonly record struct PasswordCharacterTypeRuleEnum : IStringEnum
{
    public static readonly PasswordCharacterTypeRuleEnum All = new(Values.All);

    public static readonly PasswordCharacterTypeRuleEnum ThreeOfFour = new(Values.ThreeOfFour);

    public PasswordCharacterTypeRuleEnum(string value)
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
    public static PasswordCharacterTypeRuleEnum FromCustom(string value)
    {
        return new PasswordCharacterTypeRuleEnum(value);
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

    public static bool operator ==(PasswordCharacterTypeRuleEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PasswordCharacterTypeRuleEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PasswordCharacterTypeRuleEnum value) => value.Value;

    public static explicit operator PasswordCharacterTypeRuleEnum(string value) => new(value);

    internal class PasswordCharacterTypeRuleEnumSerializer
        : JsonConverter<PasswordCharacterTypeRuleEnum>
    {
        public override PasswordCharacterTypeRuleEnum Read(
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
            return new PasswordCharacterTypeRuleEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PasswordCharacterTypeRuleEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PasswordCharacterTypeRuleEnum ReadAsPropertyName(
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
            return new PasswordCharacterTypeRuleEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PasswordCharacterTypeRuleEnum value,
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
        public const string All = "all";

        public const string ThreeOfFour = "three_of_four";
    }
}
