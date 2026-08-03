using Auth0.MyAccountApi.Core;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi.ConnectedAccounts;

[Serializable]
public record ListConnectedAccountsConnectionsRequestParameters
{
    /// <summary>
    /// Cursor for pagination - start retrieving results from this point
    /// </summary>
    [JsonIgnore]
    public Optional<string?> From { get; set; }

    /// <summary>
    /// Number of results to return (1-20)
    /// </summary>
    [JsonIgnore]
    public Optional<int?> Take { get; set; } = 10;

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
