using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record AuthenticationMethodMfaBase : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The authentication method status
    /// </summary>
    [JsonPropertyName("confirmed")]
    public required bool Confirmed { get; set; }

    /// <summary>
    /// The date and time when the authentication method was last used
    /// </summary>
    [Optional]
    [JsonPropertyName("last_auth_at")]
    public DateTime? LastAuthAt { get; set; }

    /// <summary>
    /// The unique identifier of the authentication method
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The date and time when the authentication method was created
    /// </summary>
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("usage")]
    public IEnumerable<UsageEnum> Usage { get; set; } = new List<UsageEnum>();

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
