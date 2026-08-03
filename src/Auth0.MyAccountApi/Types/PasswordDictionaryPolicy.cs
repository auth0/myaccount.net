using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Dictionary checking settings to block weak or banned passwords
/// </summary>
[Serializable]
public record PasswordDictionaryPolicy : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether dictionary checking is enabled
    /// </summary>
    [JsonPropertyName("active")]
    public required bool Active { get; set; }

    [JsonPropertyName("default")]
    public required PasswordDictionaryDefaultEnum Default { get; set; }

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
