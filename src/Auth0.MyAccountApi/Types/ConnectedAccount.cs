using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record ConnectedAccount : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The unique identifier for the connected account.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The name of the connection associated with the account.
    /// </summary>
    [JsonPropertyName("connection")]
    public required string Connection { get; set; }

    [JsonPropertyName("access_type")]
    public required ConnectedAccountAccessTypeEnum AccessType { get; set; }

    /// <summary>
    /// The scopes granted for this connected account.
    /// </summary>
    [JsonPropertyName("scopes")]
    public IEnumerable<string> Scopes { get; set; } = new List<string>();

    /// <summary>
    /// ISO 8601 timestamp when the connected account was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// ISO 8601 timestamp when the connected account expires.
    /// </summary>
    [Optional]
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// The organization ID this connected account is scoped to. Only present for accounts bound to an organization.
    /// </summary>
    [Optional]
    [JsonPropertyName("org_id")]
    public string? OrgId { get; set; }

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
