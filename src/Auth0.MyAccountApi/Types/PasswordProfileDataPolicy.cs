using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Settings for blocking personal information in passwords
/// </summary>
[Serializable]
public record PasswordProfileDataPolicy : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether to block personal information (name, email, etc.) in passwords
    /// </summary>
    [JsonPropertyName("active")]
    public required bool Active { get; set; }

    /// <summary>
    /// User profile fields to block from passwords (e.g., 'name', 'email', 'user_metadata.first')
    /// </summary>
    [JsonPropertyName("blocked_fields")]
    public IEnumerable<string> BlockedFields { get; set; } = new List<string>();

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
