using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[JsonConverter(
    typeof(CreateConnectedAccountsRequestContentCodeChallengeMethod.CreateConnectedAccountsRequestContentCodeChallengeMethodSerializer)
)]
[Serializable]
public readonly record struct CreateConnectedAccountsRequestContentCodeChallengeMethod : IStringEnum
{
    public static readonly CreateConnectedAccountsRequestContentCodeChallengeMethod S256 = new(
        Values.S256
    );

    public CreateConnectedAccountsRequestContentCodeChallengeMethod(string value)
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
    public static CreateConnectedAccountsRequestContentCodeChallengeMethod FromCustom(string value)
    {
        return new CreateConnectedAccountsRequestContentCodeChallengeMethod(value);
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

    public static bool operator ==(
        CreateConnectedAccountsRequestContentCodeChallengeMethod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateConnectedAccountsRequestContentCodeChallengeMethod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateConnectedAccountsRequestContentCodeChallengeMethod value
    ) => value.Value;

    public static explicit operator CreateConnectedAccountsRequestContentCodeChallengeMethod(
        string value
    ) => new(value);

    internal class CreateConnectedAccountsRequestContentCodeChallengeMethodSerializer
        : JsonConverter<CreateConnectedAccountsRequestContentCodeChallengeMethod>
    {
        public override CreateConnectedAccountsRequestContentCodeChallengeMethod Read(
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
            return new CreateConnectedAccountsRequestContentCodeChallengeMethod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateConnectedAccountsRequestContentCodeChallengeMethod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreateConnectedAccountsRequestContentCodeChallengeMethod ReadAsPropertyName(
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
            return new CreateConnectedAccountsRequestContentCodeChallengeMethod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateConnectedAccountsRequestContentCodeChallengeMethod value,
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
        public const string S256 = "S256";
    }
}
