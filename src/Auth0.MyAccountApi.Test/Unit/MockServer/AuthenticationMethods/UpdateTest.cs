using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Test.Unit.MockServer;
using Auth0.MyAccountApi.Test.Utils;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.AuthenticationMethods;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {}
            """;

        const string mockResponse = """
            {
              "type": "password",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "identity_user_id": "identity_user_id",
              "last_password_reset": "2024-01-15T09:30:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.UpdateAsync(
            "authentication_method_id",
            new UpdateAuthenticationMethodRequestContent()
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
