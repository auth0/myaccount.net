using Auth0.MyAccountApi.Core;

namespace Auth0.MyAccountApi;

/// <summary>
/// Auth0 My Account API client with automatic token management.
/// </summary>
/// <remarks>
/// This is the recommended entry point for the Auth0 My Account API. It wraps the
/// low-level <see cref="MyAccountApiClient"/> and adds token management via
/// <see cref="ITokenProvider"/>.
/// <para>
/// Configure the target tenant by setting <see cref="MyAccountClientOptions.Domain"/>
/// (e.g., <c>"your-tenant.auth0.com"</c>). The Base URL is constructed automatically as
/// <c>https://{Domain}/me/v1</c>. Alternatively, set
/// <see cref="MyAccountClientOptions.BaseUrl"/> directly to override this behaviour.
/// </para>
/// <para>
/// The My Account API operates in the context of the signed-in end user, so the
/// token supplied by the <see cref="ITokenProvider"/> must be that user's access token:
/// <list type="bullet">
///   <item><see cref="DelegateTokenProvider"/> — retrieve the user's token from a custom async source.</item>
/// </list>
/// </para>
/// Example:
/// <code>
/// var client = new MyAccountClient(new MyAccountClientOptions
/// {
///     Domain = "tenant.auth0.com",
///     TokenProvider = new DelegateTokenProvider(ct => GetCurrentUserAccessTokenAsync(ct))
/// });
/// </code>
/// </remarks>
public sealed class MyAccountClient : MyAccountApiClient, IDisposable
{
    private readonly HttpClient? _ownedHttpClient;
    private readonly ITokenProvider _tokenProvider;

    /// <summary>
    /// Creates a new <see cref="MyAccountClient"/> instance.
    /// </summary>
    /// <param name="options">Configuration options for the client.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="options"/> is null or when
    /// <see cref="MyAccountClientOptions.TokenProvider"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when neither <see cref="MyAccountClientOptions.Domain"/> nor
    /// <see cref="MyAccountClientOptions.BaseUrl"/> is set.
    /// </exception>
    public MyAccountClient(MyAccountClientOptions options)
        : this(Validate(options), BuildClientOptions(options))
    {
    }

    private MyAccountClient(MyAccountClientOptions options, ClientOptions clientOptions)
        : base(null, clientOptions)
    {
        _tokenProvider = options.TokenProvider;
        _ownedHttpClient = options.HttpClient == null ? clientOptions.HttpClient : null;

        // Replace the static "Bearer " auth header with a dynamic per-request supplier.
        SetAuthorizationHeader(async () =>
            $"Bearer {await _tokenProvider.GetTokenAsync(default).ConfigureAwait(false)}");
    }

    private static MyAccountClientOptions Validate(MyAccountClientOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));
        if (options.TokenProvider == null)
            throw new ArgumentNullException(
                nameof(MyAccountClientOptions.TokenProvider), "TokenProvider must not be null.");
        if (string.IsNullOrWhiteSpace(options.Domain) && string.IsNullOrWhiteSpace(options.BaseUrl))
            throw new ArgumentException(
                "Either Domain or BaseUrl must be set.",
                nameof(MyAccountClientOptions.Domain));

        if (!string.IsNullOrWhiteSpace(options.Domain) && string.IsNullOrWhiteSpace(options.BaseUrl))
        {
            var domain = options.Domain.Trim();
            if (domain.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                domain.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(
                    "Domain must not include a URL scheme (e.g. use \"tenant.auth0.com\", not \"https://tenant.auth0.com\").",
                    nameof(MyAccountClientOptions.Domain));
            if (domain.Contains('/'))
                throw new ArgumentException(
                    "Domain must not include a path or trailing slash (e.g. use \"tenant.auth0.com\").",
                    nameof(MyAccountClientOptions.Domain));
        }

        return options;
    }

    private static ClientOptions BuildClientOptions(MyAccountClientOptions options)
    {
        var domain = options.Domain?.Trim();
        return new ClientOptions
        {
            BaseUrl = options.BaseUrl ?? $"https://{domain}/me/v1",
            HttpClient = options.HttpClient ?? new HttpClient(),
            Timeout = options.Timeout ?? TimeSpan.FromSeconds(30),
            MaxRetries = options.MaxRetries ?? 2,
            AdditionalHeaders = options.AdditionalHeaders?.ToList() ?? [],
        };
    }

    /// <summary>
    /// Disposes the <see cref="MyAccountClient"/> and releases managed resources.
    /// Only disposes the internal <see cref="HttpClient"/> if it was created by this instance.
    /// The <see cref="ITokenProvider"/> is not disposed — the caller owns it and is responsible for its lifetime.
    /// </summary>
    public void Dispose()
    {
        _ownedHttpClient?.Dispose();
    }
}
