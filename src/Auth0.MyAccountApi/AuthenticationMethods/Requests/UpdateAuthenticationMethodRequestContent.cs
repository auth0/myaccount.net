using Auth0.MyAccountApi.Core;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record UpdateAuthenticationMethodRequestContent
{
    /// <summary>
    /// The friendly name of the authentication method
    /// </summary>
    [Optional]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The preferred authentication method (for phone authenticators)
    /// </summary>
    [Optional]
    [JsonPropertyName("preferred_authentication_method")]
    public PhoneAuthenticationMethodEnum? PreferredAuthenticationMethod { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
