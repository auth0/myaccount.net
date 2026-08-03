namespace Auth0.MyAccountApi;

public partial interface IAuthenticationMethodsClient
{
    /// <summary>
    /// Retrieve detailed list of authentication methods belonging to the authenticated user.
    /// </summary>
    WithRawResponseTask<ListAuthenticationMethodsResponseContent> ListAsync(
        ListAuthenticationMethodsRequestParameters request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Start the enrollment of a supported authentication method.
    /// </summary>
    WithRawResponseTask<CreateAuthenticationMethodResponseContent> CreateAsync(
        CreateAuthenticationMethodRequestContent request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Retrieves a single authentication method belonging to the authenticated user.
    /// </summary>
    WithRawResponseTask<GetAuthenticationMethodResponseContent> GetAsync(
        string authenticationMethodId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a single authentication method belonging to the authenticated user.
    /// </summary>
    WithRawResponseTask DeleteAsync(
        string authenticationMethodId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates a single authentication method
    /// </summary>
    WithRawResponseTask<UpdateAuthenticationMethodResponseContent> UpdateAsync(
        string authenticationMethodId,
        UpdateAuthenticationMethodRequestContent request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Confirm the enrollment of a supported authentication method.
    /// </summary>
    WithRawResponseTask<VerifyAuthenticationMethodResponseContent> VerifyAsync(
        string authenticationMethodId,
        VerifyAuthenticationMethodRequestContent request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
