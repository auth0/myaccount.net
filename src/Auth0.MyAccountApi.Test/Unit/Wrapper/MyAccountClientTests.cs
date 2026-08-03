using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace Auth0.MyAccountApi.Test.Unit.Wrapper;

[TestFixture]
public class MyAccountClientTests
{
    private WireMockServer _server = null!;

    private const string FactorsResponse = """
        {
            "factors": [
                {
                    "type": "password",
                    "usage": [ "primary" ]
                }
            ]
        }
        """;

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start(new WireMockServerSettings());
    }

    [TearDown]
    public void TearDown()
    {
        _server.Stop();
        _server.Dispose();
    }

    private void StubFactors()
    {
        _server
            .Given(Request.Create().WithPath("/factors").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithBody(FactorsResponse));
    }

    [Test]
    public void Constructor_NullOptions_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new MyAccountClient(null!));
    }

    [Test]
    public void Constructor_NullTokenProvider_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new MyAccountClient(new MyAccountClientOptions
            {
                TokenProvider = null!
            }));
    }

    [Test]
    public void Constructor_NoDomainOrBaseUrl_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new MyAccountClient(new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
            }));
    }

    [Test]
    public async Task Request_IncludesBearerAuthorizationHeader()
    {
        StubFactors();

        using var client = new MyAccountClient(new MyAccountClientOptions
        {
            TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("my-dynamic-token")),
            BaseUrl = _server.Urls[0],
            MaxRetries = 0,
        });

        await client.Factors.ListAsync();

        var logEntry = _server.LogEntries.Single();
        Assert.That(
            logEntry.RequestMessage.Headers!["Authorization"].First(),
            Is.EqualTo("Bearer my-dynamic-token"));
    }

    [Test]
    public async Task Request_TokenFetchedFreshOnEveryRequest()
    {
        StubFactors();

        var callCount = 0;
        using var client = new MyAccountClient(new MyAccountClientOptions
        {
            TokenProvider = new DelegateTokenProvider(_ =>
            {
                callCount++;
                return Task.FromResult($"token-{callCount}");
            }),
            BaseUrl = _server.Urls[0],
            MaxRetries = 0,
        });

        await client.Factors.ListAsync();
        await client.Factors.ListAsync();

        // Token delegate should be called once per request
        Assert.That(callCount, Is.EqualTo(2));
        var entries = _server.LogEntries.ToList();
        Assert.That(entries[0].RequestMessage.Headers!["Authorization"].First(), Is.EqualTo("Bearer token-1"));
        Assert.That(entries[1].RequestMessage.Headers!["Authorization"].First(), Is.EqualTo("Bearer token-2"));
    }

    [Test]
    public async Task Request_AdditionalHeadersAreSent()
    {
        StubFactors();

        using var client = new MyAccountClient(new MyAccountClientOptions
        {
            TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
            BaseUrl = _server.Urls[0],
            MaxRetries = 0,
            AdditionalHeaders = new Dictionary<string, string?>
            {
                { "X-Custom-Header", "custom-value" }
            }
        });

        await client.Factors.ListAsync();

        var logEntry = _server.LogEntries.Single();
        Assert.That(
            logEntry.RequestMessage.Headers!["X-Custom-Header"].First(),
            Is.EqualTo("custom-value"));
    }

    [Test]
    public async Task Request_BaseUrlTakesPrecedenceOverDomain()
    {
        _server
            .Given(Request.Create().WithPath("/custom-base/factors").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithBody(FactorsResponse));

        using var client = new MyAccountClient(new MyAccountClientOptions
        {
            TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
            Domain = "should.be.ignored.auth0.com",
            BaseUrl = $"{_server.Urls[0]}/custom-base",
            MaxRetries = 0,
        });

        await client.Factors.ListAsync();

        var logEntry = _server.LogEntries.Single();
        Assert.That(logEntry.RequestMessage.Path, Is.EqualTo("/custom-base/factors"));
    }

    [TestCase("https://tenant.auth0.com")]
    [TestCase("http://tenant.auth0.com")]
    [TestCase("HTTPS://tenant.auth0.com")]
    public void Constructor_DomainWithScheme_ThrowsArgumentException(string domain)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new MyAccountClient(new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
                Domain = domain,
            }));
        Assert.That(ex!.ParamName, Is.EqualTo(nameof(MyAccountClientOptions.Domain)));
    }

    [TestCase("tenant.auth0.com/")]
    [TestCase("tenant.auth0.com/me/v1")]
    public void Constructor_DomainWithSlash_ThrowsArgumentException(string domain)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new MyAccountClient(new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
                Domain = domain,
            }));
        Assert.That(ex!.ParamName, Is.EqualTo(nameof(MyAccountClientOptions.Domain)));
    }

    [TestCase(" tenant.auth0.com ")]
    [TestCase("\ttenant.auth0.com\t")]
    public void Constructor_DomainWithWhitespace_DoesNotThrow(string domain)
    {
        Assert.DoesNotThrow(() =>
        {
            using var client = new MyAccountClient(new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
                Domain = domain,
            });
        });
    }

    [Test]
    public void Constructor_DomainWithScheme_WhenBaseUrlAlsoSet_DoesNotThrow()
    {
        // BaseUrl takes precedence; Domain is ignored so no validation runs on it
        Assert.DoesNotThrow(() =>
        {
            using var client = new MyAccountClient(new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
                Domain = "https://tenant.auth0.com",
                BaseUrl = "https://tenant.auth0.com/me/v1",
            });
        });
    }

    [Test]
    public void Dispose_DoesNotThrow()
    {
        var client = new MyAccountClient(new MyAccountClientOptions
        {
            TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
            Domain = "tenant.auth0.com",
        });

        Assert.DoesNotThrow(() => client.Dispose());
    }

    [Test]
    public void Dispose_WithProvidedHttpClient_DoesNotDisposeIt()
    {
        // When caller provides HttpClient, MyAccountClient must not own/dispose it
        var httpClient = new HttpClient();
        var client = new MyAccountClient(new MyAccountClientOptions
        {
            TokenProvider = new DelegateTokenProvider(_ => Task.FromResult("token")),
            Domain = "tenant.auth0.com",
            HttpClient = httpClient,
        });

        client.Dispose();

        // HttpClient should still be usable after client is disposed
        Assert.DoesNotThrow(() => httpClient.BaseAddress = new Uri("https://example.com"));
        httpClient.Dispose();
    }
}
