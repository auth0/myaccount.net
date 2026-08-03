using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record PublicKeyCredentialCreationOptions : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [Optional]
    [JsonPropertyName("authenticatorSelection")]
    public AuthenticatorSelection? AuthenticatorSelection { get; set; }

    [JsonPropertyName("challenge")]
    public required string Challenge { get; set; }

    [JsonPropertyName("pubKeyCredParams")]
    public IEnumerable<PublicKeyCredentialParameter> PubKeyCredParams { get; set; } =
        new List<PublicKeyCredentialParameter>();

    [JsonPropertyName("rp")]
    public required RelyingParty Rp { get; set; }

    [Optional]
    [JsonPropertyName("timeout")]
    public double? Timeout { get; set; }

    [JsonPropertyName("user")]
    public required User User { get; set; }

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
