namespace Auth0.MyAccountApi;

/// <summary>
/// An <see cref="ITokenProvider"/> backed by a user-supplied async delegate.
/// Use this to retrieve the end-user access token from an external source such as
/// the current session, a secure store, a cache, or any custom async operation.
/// The delegate is invoked on every call; no caching is performed by this class.
/// </summary>
public sealed class DelegateTokenProvider : ITokenProvider
{
    private readonly Func<CancellationToken, Task<string>> _tokenFactory;

    /// <summary>
    /// Creates a new <see cref="DelegateTokenProvider"/> with a cancellable async token factory.
    /// </summary>
    /// <param name="tokenFactory">
    /// An async function that accepts a <see cref="CancellationToken"/> and returns a valid access token.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="tokenFactory"/> is null.</exception>
    public DelegateTokenProvider(Func<CancellationToken, Task<string>> tokenFactory)
    {
        _tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
    }

    /// <inheritdoc />
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        var token = await _tokenFactory(cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(token))
            throw new InvalidOperationException(
                "The token factory returned a null or empty token. Ensure the delegate returns a valid access token.");
        return token;
    }
}
