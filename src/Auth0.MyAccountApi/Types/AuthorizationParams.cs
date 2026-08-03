using Auth0.MyAccountApi.Core;
using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

/// <summary>
/// Authorization parameters to be sent to the underlying Identity Provider (IdP) during the authorization request. These parameters influence how the IdP processes the authorization flow and what permissions are requested.
/// </summary>
[Serializable]
public record AuthorizationParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Specifies the desired Authentication Context Class Reference values, which indicate the required level of authentication (e.g., multi-factor, passwordless). The Identity Provider (IdP) may enforce a specific assurance level based on these values.
    /// </summary>
    [Optional]
    [JsonPropertyName("acr_values")]
    public string? AcrValues { get; set; }

    /// <summary>
    /// Defines the intended recipient of the access token, typically indicating who the token is meant for (e.g., an API identifier).
    /// </summary>
    [Optional]
    [JsonPropertyName("audience")]
    public string? Audience { get; set; }

    /// <summary>
    /// Specifies which resource server the client is requesting access to (used in OAuth 2.0 Resource Indicators).
    /// </summary>
    [Optional]
    [JsonPropertyName("resource")]
    public string? Resource { get; set; }

    [Optional]
    [JsonPropertyName("display")]
    public DisplayEnum? Display { get; set; }

    /// <summary>
    /// Provides a previously issued id_token to the IdP. It can be used to suggest which user is being authenticated or to facilitate logout requests.
    /// </summary>
    [Optional]
    [JsonPropertyName("id_token_hint")]
    public string? IdTokenHint { get; set; }

    /// <summary>
    /// Supplies a hint about the user's identifier (like an email address or username) to pre-populate the login form.
    /// </summary>
    [Optional]
    [JsonPropertyName("login_hint")]
    public string? LoginHint { get; set; }

    /// <summary>
    /// Indicates the maximum time (in seconds) since the user's last authentication. If exceeded, the IdP forces the user to re-authenticate.
    /// </summary>
    [Optional]
    [JsonPropertyName("max_age")]
    public int? MaxAge { get; set; }

    [Optional]
    [JsonPropertyName("prompt")]
    public PromptEnum? Prompt { get; set; }

    /// <summary>
    /// Suggests the preferred languages for the user interface, passed as a space-separated list of BCP47 language tags (e.g., 'en', 'es', 'fr').
    /// </summary>
    [Optional]
    [JsonPropertyName("ui_locales")]
    public string? UiLocales { get; set; }

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
