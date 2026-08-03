using Auth0.MyAccountApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.ConnectedAccounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class DeleteTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/connected-accounts/accounts/id")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () => await Client.ConnectedAccounts.DeleteAsync("id"));
    }
}
