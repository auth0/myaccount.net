using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record CreateConnectedAccountsResponseContent : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The base URI to initiate the account connection flow.
    /// </summary>
    [JsonPropertyName("connect_uri")]
    public required string ConnectUri { get; set; }

    /// <summary>
    /// The authentication session identifier.
    /// </summary>
    [JsonPropertyName("auth_session")]
    public required string AuthSession { get; set; }

    [JsonPropertyName("connect_params")]
    public required ConnectParams ConnectParams { get; set; }

    /// <summary>
    /// The number of seconds until the ticket expires.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public required int ExpiresIn { get; set; }

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
