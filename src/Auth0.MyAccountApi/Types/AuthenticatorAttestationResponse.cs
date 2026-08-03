using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record AuthenticatorAttestationResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [Optional]
    [JsonPropertyName("authenticatorAttachment")]
    public AuthenticatorAttachmentEnum? AuthenticatorAttachment { get; set; }

    [Optional]
    [JsonPropertyName("clientExtensionResults")]
    public Dictionary<string, object?>? ClientExtensionResults { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("rawId")]
    public required string RawId { get; set; }

    [JsonPropertyName("response")]
    public required AuthenticatorAttestationResponseData Response { get; set; }

    [JsonPropertyName("type")]
    public required AuthenticatorAttestationResponseType Type { get; set; }

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
