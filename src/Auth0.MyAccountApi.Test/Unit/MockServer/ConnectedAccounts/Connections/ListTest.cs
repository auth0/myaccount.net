using Auth0.MyAccountApi.ConnectedAccounts;
using Auth0.MyAccountApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.ConnectedAccounts.Connections;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "connections": [
                {
                  "name": "name",
                  "strategy": "strategy",
                  "scopes": [
                    "scopes"
                  ]
                }
              ],
              "next": "next"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/connected-accounts/connections")
                    .WithParam("from", "from")
                    .WithParam("take", "1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var items = await Client.ConnectedAccounts.Connections.ListAsync(
            new ListConnectedAccountsConnectionsRequestParameters { From = "from", Take = 1 }
        );
        await foreach (var item in items)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
