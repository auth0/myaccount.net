using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Test.Unit.MockServer;
using Auth0.MyAccountApi.Test.Utils;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.ConnectedAccounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CompleteTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "auth_session": "auth_session",
              "connect_code": "connect_code",
              "redirect_uri": "redirect_uri"
            }
            """;

        const string mockResponse = """
            {
              "id": "id",
              "connection": "connection",
              "access_type": "offline",
              "scopes": [
                "scopes"
              ],
              "created_at": "2024-01-15T09:30:00.000Z",
              "expires_at": "2024-01-15T09:30:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/connected-accounts/complete")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConnectedAccounts.CompleteAsync(
            new CompleteConnectedAccountsRequestContent
            {
                AuthSession = "auth_session",
                ConnectCode = "connect_code",
                RedirectUri = "redirect_uri",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
