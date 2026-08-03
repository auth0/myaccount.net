using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Test.Unit.MockServer;
using Auth0.MyAccountApi.Test.Utils;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.ConnectedAccounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "connection": "connection",
              "redirect_uri": "redirect_uri"
            }
            """;

        const string mockResponse = """
            {
              "connect_uri": "connect_uri",
              "auth_session": "auth_session",
              "connect_params": {
                "ticket": "ticket"
              },
              "expires_in": 1
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/connected-accounts/connect")
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

        var response = await Client.ConnectedAccounts.CreateAsync(
            new CreateConnectedAccountsRequestContent
            {
                Connection = "connection",
                RedirectUri = "redirect_uri",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
