using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record AuthenticationMethodPassword : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The ID of the user identity linked with the authentication method
    /// </summary>
    [JsonPropertyName("identity_user_id")]
    public required string IdentityUserId { get; set; }

    /// <summary>
    /// The date of the last password reset
    /// </summary>
    [Optional]
    [JsonPropertyName("last_password_reset")]
    public DateTime? LastPasswordReset { get; set; }

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
