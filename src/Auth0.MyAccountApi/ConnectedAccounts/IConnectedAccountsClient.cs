using Auth0.MyAccountApi.ConnectedAccounts;
using Auth0.MyAccountApi.Core;

namespace Auth0.MyAccountApi;

public partial interface IConnectedAccountsClient
{
    public IConnectionsClient Connections { get; }

    /// <summary>
    /// Start an authorization flow to link the authenticated user's account with an external identity provider.
    /// </summary>
    WithRawResponseTask<CreateConnectedAccountsResponseContent> CreateAsync(
        CreateConnectedAccountsRequestContent request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Complete a previously started authorization flow to link the authenticated user's account with an external identity provider.
    /// </summary>
    WithRawResponseTask<CompleteConnectedAccountsResponseContent> CompleteAsync(
        CompleteConnectedAccountsRequestContent request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Retrieve connected accounts belonging to the authenticated user.
    /// </summary>
    Task<Pager<ConnectedAccount>> ListAsync(
        ListConnectedAccountsRequestParameters request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Delete a connected account belonging to the authenticated user.
    /// </summary>
    WithRawResponseTask DeleteAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
