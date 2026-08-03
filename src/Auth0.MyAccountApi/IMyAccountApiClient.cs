namespace Auth0.MyAccountApi;

public partial interface IMyAccountApiClient
{
    public IFactorsClient Factors { get; }
    public IAuthenticationMethodsClient AuthenticationMethods { get; }
    public IConnectedAccountsClient ConnectedAccounts { get; }
}
