using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record QrCodeCreationResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The URI for the QR code to be scanned by the authenticator.
    /// </summary>
    [JsonPropertyName("barcode_uri")]
    public required string BarcodeUri { get; set; }

    /// <summary>
    /// The manual input code for the authenticator in case QR codes cannot be used.
    /// </summary>
    [Optional]
    [JsonPropertyName("manual_input_code")]
    public string? ManualInputCode { get; set; }

    /// <summary>
    /// The unique identifier for the authentication method.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The unique session identifier for the enrollment.
    /// </summary>
    [JsonPropertyName("auth_session")]
    public required string AuthSession { get; set; }

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
