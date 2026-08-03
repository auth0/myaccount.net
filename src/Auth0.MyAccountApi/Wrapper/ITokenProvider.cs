namespace Auth0.MyAccountApi;

/// <summary>
/// Provides access tokens for API authentication.
/// Implement this interface to supply the end-user access token used to call the
/// Auth0 My Account API from any source: an in-memory session, a secure store,
/// a custom cache, or a refresh-token exchange.
/// <list type="bullet">
///   <item><see cref="DelegateTokenProvider"/> — retrieve tokens from a custom async source.</item>
/// </list>
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// Returns a valid access token, fetching or refreshing as needed.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
}
