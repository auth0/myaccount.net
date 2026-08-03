using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Password complexity requirements
/// </summary>
[Serializable]
public record PasswordComplexityPolicy : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Minimum password length
    /// </summary>
    [JsonPropertyName("min_length")]
    public required int MinLength { get; set; }

    /// <summary>
    /// Required character types
    /// </summary>
    [JsonPropertyName("character_types")]
    public IEnumerable<PasswordCharacterTypeEnum> CharacterTypes { get; set; } =
        new List<PasswordCharacterTypeEnum>();

    [JsonPropertyName("character_type_rule")]
    public required PasswordCharacterTypeRuleEnum CharacterTypeRule { get; set; }

    [JsonPropertyName("identical_characters")]
    public required PasswordIdenticalCharactersEnum IdenticalCharacters { get; set; }

    [JsonPropertyName("sequential_characters")]
    public required PasswordSequentialCharactersEnum SequentialCharacters { get; set; }

    [JsonPropertyName("max_length_exceeded")]
    public required PasswordMaxLengthExceededEnum MaxLengthExceeded { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
