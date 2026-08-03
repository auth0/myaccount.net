using Auth0.MyAccountApi.Core;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record CompleteConnectedAccountsRequestContent
{
    /// <summary>
    /// The authentication session identifier
    /// </summary>
    [JsonPropertyName("auth_session")]
    public required string AuthSession { get; set; }

    /// <summary>
    /// The authorization code returned from the connect flow
    /// </summary>
    [JsonPropertyName("connect_code")]
    public required string ConnectCode { get; set; }

    /// <summary>
    /// The redirect URI used in the original request
    /// </summary>
    [JsonPropertyName("redirect_uri")]
    public required string RedirectUri { get; set; }

    /// <summary>
    /// The PKCE code verifier
    /// </summary>
    [Optional]
    [JsonPropertyName("code_verifier")]
    public string? CodeVerifier { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
