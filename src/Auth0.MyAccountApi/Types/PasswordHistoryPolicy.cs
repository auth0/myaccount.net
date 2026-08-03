using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Password history enforcement settings
/// </summary>
[Serializable]
public record PasswordHistoryPolicy : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether password history is enforced
    /// </summary>
    [JsonPropertyName("active")]
    public required bool Active { get; set; }

    /// <summary>
    /// Number of previous passwords to remember and prevent reuse
    /// </summary>
    [JsonPropertyName("size")]
    public required int Size { get; set; }

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
