using Auth0.MyAccountApi;
using Auth0.MyAccountApi.Test.Unit.MockServer;
using Auth0.MyAccountApi.Test.Utils;
using NUnit.Framework;

namespace Auth0.MyAccountApi.Test.Unit.MockServer.AuthenticationMethods;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "type": "email",
              "email": "email"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Email(
                    new CreateEmailAuthenticationMethod { Email = "email" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_3()
    {
        const string requestJson = """
            {
              "type": "totp"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Totp(
                    new CreateTotpAuthenticationMethod()
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_4()
    {
        const string requestJson = """
            {
              "type": "push-notification"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.PushNotification(
                    new CreatePushNotificationAuthenticationMethod()
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_5()
    {
        const string requestJson = """
            {
              "type": "recovery-code"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.RecoveryCode(
                    new CreateRecoveryCodeAuthenticationMethod()
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_6()
    {
        const string requestJson = """
            {
              "type": "password"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Password(
                    new CreatePasswordAuthenticationMethod()
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_7()
    {
        const string requestJson = """
            {
              "type": "passkey"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Passkey(
                    new CreatePasskeyAuthenticationMethod()
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_8()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "phone|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_9()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "email|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_10()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "totp|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed...",
              "barcode_uri": "otpauth://totp/my-tenant:auth0%7C507f1f77bcf86cd799439011?secret=JBSWY3DPEHPK3PXP&issuer=my-tenant&algorithm=SHA1&digits=6&period=30",
              "manual_input_code": "JBSWY3DPEHPK3PXP"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_11()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "push-notification|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed...",
              "barcode_uri": "otpauth://totp/my-tenant:auth0%7C507f1f77bcf86cd799439011?secret=JBSWY3DPEHPK3PXP&issuer=my-tenant&algorithm=SHA1&digits=6&period=30",
              "manual_input_code": "manual_input_code"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_12()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "recovery-code|dev_XXXXXXXXXXXXXXXX",
              "auth_session": "Fe26.2**05c400ed...",
              "recovery_code": "ABCDEFGHIJKLMNOPQRSTUVWX"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_13()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "id": "password|new",
              "policy": {
                "complexity": {
                  "min_length": 8,
                  "character_types": [
                    "lowercase",
                    "uppercase",
                    "number",
                    "special"
                  ],
                  "character_type_rule": "three_of_four",
                  "identical_characters": "block",
                  "sequential_characters": "block",
                  "max_length_exceeded": "truncate"
                },
                "profile_data": {
                  "active": true,
                  "blocked_fields": [
                    "name",
                    "email"
                  ]
                },
                "history": {
                  "active": true,
                  "size": 5
                },
                "dictionary": {
                  "active": true,
                  "default": "en_10k"
                }
              },
              "auth_session": "Fe26.2**05c400ed..."
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_14()
    {
        const string requestJson = """
            {
              "type": "phone",
              "phone_number": "phone_number"
            }
            """;

        const string mockResponse = """
            {
              "auth_session": "Fe26.2**05c400ed...",
              "authn_params_public_key": {
                "authenticatorSelection": {
                  "residentKey": "required",
                  "userVerification": "preferred"
                },
                "challenge": "dGVzdC1jaGFsbGVuZ2U",
                "pubKeyCredParams": [
                  {
                    "alg": -7,
                    "type": "public-key"
                  },
                  {
                    "alg": -257,
                    "type": "public-key"
                  }
                ],
                "rp": {
                  "id": "example.auth0.com",
                  "name": "My Application"
                },
                "timeout": 60000,
                "user": {
                  "id": "YXV0aDB8NTA3ZjFmNzdiY2Y4NmNkNzk5NDM5MDEx",
                  "name": "user@example.com",
                  "displayName": "User"
                }
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authentication-methods")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.AuthenticationMethods.CreateAsync(
            new CreateAuthenticationMethodRequestContent(
                new CreateAuthenticationMethodRequestContent.Phone(
                    new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
                )
            )
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
