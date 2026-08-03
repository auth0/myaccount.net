namespace Auth0.MyAccountApi;

public partial class MyAccountApiClient
{
    /// <summary>
    /// Replaces the Authorization header with a dynamic supplier that is evaluated per-request.
    /// Used by <see cref="MyAccountClient"/> to wire in <see cref="ITokenProvider"/> support.
    /// </summary>
    internal void SetAuthorizationHeader(Func<Task<string>> supplier)
    {
        _client.Options.Headers["Authorization"] = supplier;
    }
}
