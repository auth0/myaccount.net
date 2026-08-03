using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Core;

namespace Auth0.MyAccountApi.ConnectedAccounts;

public partial interface IConnectionsClient
{
    /// <summary>
    /// Retrieve available connections that can be used for account linking by the authenticated user.
    /// </summary>
    Task<Pager<ConnectedAccountConnection>> ListAsync(
        ListConnectedAccountsConnectionsRequestParameters request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
