using Auth0.MyAccountApi.Core;

namespace Auth0.MyAccountApi;

public partial class MyAccountApiClient : IMyAccountApiClient
{
    private readonly RawClient _client;

    public MyAccountApiClient(string? token = null, ClientOptions? clientOptions = null)
    {
        clientOptions ??= new ClientOptions();
        var platformHeaders = new Headers(new Dictionary<string, string>() { });
        foreach (var header in platformHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        var clientOptionsWithAuth = clientOptions.Clone();
        var authHeaders = new Headers(
            new Dictionary<string, string>() { { "Authorization", $"Bearer {token ?? ""}" } }
        );
        foreach (var header in authHeaders)
        {
            clientOptionsWithAuth.Headers[header.Key] = header.Value;
        }
        _client = new RawClient(clientOptionsWithAuth);
        Factors = new FactorsClient(_client);
        AuthenticationMethods = new AuthenticationMethodsClient(_client);
        ConnectedAccounts = new ConnectedAccountsClient(_client);
    }

    public IFactorsClient Factors { get; }

    public IAuthenticationMethodsClient AuthenticationMethods { get; }

    public IConnectedAccountsClient ConnectedAccounts { get; }
}
