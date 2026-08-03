using global::System.Net.Http;

namespace Auth0.MyAccountApi.Core;

internal static class HttpMethodExtensions
{
    public static readonly HttpMethod Patch = new("PATCH");
}
