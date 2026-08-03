using Auth0.MyAccountApi.Test.Unit.MockServer;
using Auth0.MyAccountApi.Test.Utils;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.AuthenticationMethods;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "type": "phone",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z",
              "name": "name",
              "phone_number": "phone_number",
              "preferred_authentication_method": "sms"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "type": "email",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z",
              "name": "name",
              "email": "email"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_3()
    {
        const string mockResponse = """
            {
              "type": "totp",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z",
              "name": "name"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_4()
    {
        const string mockResponse = """
            {
              "type": "push-notification",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z",
              "name": "name"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_5()
    {
        const string mockResponse = """
            {
              "type": "recovery-code",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_6()
    {
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
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_7()
    {
        const string mockResponse = """
            {
              "type": "passkey",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "credential_backed_up": true,
              "credential_device_type": "multi_device",
              "identity_user_id": "identity_user_id",
              "key_id": "key_id",
              "public_key": "public_key",
              "transports": [
                "internal"
              ],
              "user_agent": "Chrome 131.0.0 / Mac OS X 10.15.7",
              "user_handle": "user_handle",
              "aaguid": "aaguid",
              "relying_party_id": "relying_party_id",
              "last_auth_at": "2024-01-15T09:30:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_8()
    {
        const string mockResponse = """
            {
              "type": "webauthn-platform",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z",
              "name": "name",
              "key_id": "key_id",
              "public_key": "public_key"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_9()
    {
        const string mockResponse = """
            {
              "type": "webauthn-roaming",
              "id": "id",
              "created_at": "2024-01-15T09:30:00.000Z",
              "usage": [
                "primary"
              ],
              "confirmed": true,
              "last_auth_at": "2024-01-15T09:30:00.000Z",
              "name": "name",
              "key_id": "key_id",
              "public_key": "public_key"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods/authentication_method_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.GetAsync("authentication_method_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
