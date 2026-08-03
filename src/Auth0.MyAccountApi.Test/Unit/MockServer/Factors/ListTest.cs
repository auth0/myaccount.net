using Auth0.MyAccountApi.Test.Unit.MockServer;
using Auth0.MyAccountApi.Test.Utils;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.Factors;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "factors": [
                {
                  "type": "password",
                  "usage": [
                    "primary"
                  ]
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/factors").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Factors.ListAsync();
        JsonAssert.AreEqual(response, mockResponse);
    }
}
