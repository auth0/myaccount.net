using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// The password policy configuration for the connection in flexible format.
/// </summary>
[Serializable]
public record PasswordAuthenticationMethodPolicy : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("complexity")]
    public required PasswordComplexityPolicy Complexity { get; set; }

    [JsonPropertyName("profile_data")]
    public required PasswordProfileDataPolicy ProfileData { get; set; }

    [JsonPropertyName("history")]
    public required PasswordHistoryPolicy History { get; set; }

    [JsonPropertyName("dictionary")]
    public required PasswordDictionaryPolicy Dictionary { get; set; }

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
