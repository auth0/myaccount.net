using Auth0.MyAccountApi.Core;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record CreateConnectedAccountsRequestContent
{
    /// <summary>
    /// The name of the connection to link the account with (e.g., 'google-oauth2', 'facebook').
    /// </summary>
    [JsonPropertyName("connection")]
    public required string Connection { get; set; }

    /// <summary>
    /// The URI to redirect to after the connection process completes.
    /// </summary>
    [JsonPropertyName("redirect_uri")]
    public required string RedirectUri { get; set; }

    /// <summary>
    /// An opaque value used to maintain state between the request and callback.
    /// </summary>
    [Optional]
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// The PKCE code challenge derived from the code verifier.
    /// </summary>
    [Optional]
    [JsonPropertyName("code_challenge")]
    public string? CodeChallenge { get; set; }

    /// <summary>
    /// The method used to derive the code challenge. Required when code_challenge is provided.
    /// </summary>
    [Optional]
    [JsonPropertyName("code_challenge_method")]
    public CreateConnectedAccountsRequestContentCodeChallengeMethod? CodeChallengeMethod { get; set; }

    /// <summary>
    /// Defines the permissions that the client requests from the Identity Provider. Must include the standard scopes used to identify the user (e.g. 'openid', 'email', 'profile'), the scope required to obtain refresh tokens if needed (e.g. 'offline_access'), and any custom scopes the client needs to access protected resources.
    /// </summary>
    [Optional]
    [JsonPropertyName("scopes")]
    public IEnumerable<string>? Scopes { get; set; }

    [Optional]
    [JsonPropertyName("authorization_params")]
    public AuthorizationParams? AuthorizationParams { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
