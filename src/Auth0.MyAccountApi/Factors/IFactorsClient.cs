namespace Auth0.MyAccountApi;

public partial interface IFactorsClient
{
    /// <summary>
    /// List of factors enabled for the Auth0 tenant and available for enrollment by this user.
    /// </summary>
    WithRawResponseTask<ListFactorsResponseContent> ListAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
