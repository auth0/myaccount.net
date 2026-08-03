using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.ConnectedAccounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "accounts": [
                {
                  "id": "id",
                  "connection": "connection",
                  "access_type": "offline",
                  "scopes": [
                    "scopes"
                  ],
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "expires_at": "2024-01-15T09:30:00.000Z",
                  "org_id": "org_id"
                }
              ],
              "next": "next"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/connected-accounts/accounts")
                    .WithParam("connection", "connection")
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

        var items = await Client.ConnectedAccounts.ListAsync(
            new ListConnectedAccountsRequestParameters
            {
                Connection = ConnectionNamesFilter.FromString("connection"),
                From = "from",
                Take = 1,
            }
        );
        await foreach (var item in items)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
