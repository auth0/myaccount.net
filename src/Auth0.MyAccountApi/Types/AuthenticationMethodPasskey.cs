using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record AuthenticationMethodPasskey : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether the credential was backed up
    /// </summary>
    [JsonPropertyName("credential_backed_up")]
    public required bool CredentialBackedUp { get; set; }

    [JsonPropertyName("credential_device_type")]
    public required CredentialDeviceTypeEnum CredentialDeviceType { get; set; }

    /// <summary>
    /// The ID of the user identity linked with the authentication method
    /// </summary>
    [JsonPropertyName("identity_user_id")]
    public required string IdentityUserId { get; set; }

    /// <summary>
    /// The ID of the credential
    /// </summary>
    [JsonPropertyName("key_id")]
    public required string KeyId { get; set; }

    /// <summary>
    /// The public key
    /// </summary>
    [JsonPropertyName("public_key")]
    public required string PublicKey { get; set; }

    /// <summary>
    /// The transports used by clients to communicate with the authenticator
    /// </summary>
    [Optional]
    [JsonPropertyName("transports")]
    public IEnumerable<string>? Transports { get; set; }

    /// <summary>
    /// The user-agent of the browser used to create the passkey
    /// </summary>
    [Optional]
    [JsonPropertyName("user_agent")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// The user handle of the user identity
    /// </summary>
    [JsonPropertyName("user_handle")]
    public required string UserHandle { get; set; }

    /// <summary>
    /// Authenticator Attestation Globally Unique Identifier
    /// </summary>
    [Optional]
    [JsonPropertyName("aaguid")]
    public string? Aaguid { get; set; }

    /// <summary>
    /// Relying Party Identifier
    /// </summary>
    [Optional]
    [JsonPropertyName("relying_party_id")]
    public string? RelyingPartyId { get; set; }

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
