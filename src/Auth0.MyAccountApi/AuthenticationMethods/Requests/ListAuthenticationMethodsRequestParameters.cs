using Auth0.MyAccountApi.Core;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record ListAuthenticationMethodsRequestParameters
{
    /// <summary>
    /// Filter authentication methods by type
    /// </summary>
    [JsonIgnore]
    public Optional<FactorTypeEnum?> Type { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
