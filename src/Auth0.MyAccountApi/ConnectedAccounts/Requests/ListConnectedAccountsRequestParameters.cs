using Auth0.MyAccountApi.Core;
using global::System.Text.Json.Serialization;

namespace Auth0.MyAccountApi;

[Serializable]
public record ListConnectedAccountsRequestParameters
{
    /// <summary>
    /// Filter connected accounts by connection names
    /// </summary>
    [JsonIgnore]
    public Optional<ConnectionNamesFilter?> Connection { get; set; }

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
