using Auth0.MyAccountApi.ConnectedAccounts;
using Auth0.MyAccountApi.Core;
using global::System.Net.Http;
using global::System.Text;
using global::System.Text.Json;
using NUnit.Framework;
using WireMock.Server;
using WireMock.Settings;
using SystemTask = global::System.Threading.Tasks.Task;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace Auth0.MyAccountApi.Test.Core;

/// <summary>
/// Verifies the <c>Auth0-Client</c> telemetry header is present on every flow that can reach
/// the wire: the low-level client, the <see cref="MyAccountClient"/> wrapper, retried attempts,
/// paginated follow-up requests, and requests carrying per-request options.
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class TelemetryTests
{
    private const string HeaderName = "Auth0-Client";

    private const string FactorsResponse = """
        {
            "factors": [ { "type": "password", "usage": [ "primary" ] } ]
        }
        """;

    private WireMockServer _server = null!;

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
            .Given(WireMockRequest.Create().WithPath("/factors").UsingGet())
            .RespondWith(WireMockResponse.Create().WithStatusCode(200).WithBody(FactorsResponse));
    }

    /// <summary>
    /// Decodes the base64url JSON payload of an <c>Auth0-Client</c> header value.
    /// </summary>
    private static JsonElement Decode(string headerValue)
    {
        var padded = headerValue.Replace('-', '+').Replace('_', '/');
        padded += (padded.Length % 4) switch
        {
            2 => "==",
            3 => "=",
            _ => "",
        };
        var json = Encoding.UTF8.GetString(Convert.FromBase64String(padded));
        return JsonDocument.Parse(json).RootElement;
    }

    private string SingleTelemetryHeader()
    {
        var entry = _server.LogEntries.Single();
        return entry.RequestMessage.Headers![HeaderName].First();
    }

    private static void AssertWellFormed(string headerValue)
    {
        var payload = Decode(headerValue);

        Assert.That(payload.GetProperty("name").GetString(), Is.EqualTo("MyAccount.NET"));
        Assert.That(
            payload.GetProperty("version").GetString(),
            Is.EqualTo(global::Auth0.MyAccountApi.Version.Current)
        );

        var target = payload.GetProperty("env").GetProperty("target").GetString();
        Assert.That(target, Is.Not.Null.And.Not.Empty);
        Assert.That(target, Is.Not.EqualTo("UNKNOWN"), "Runtime target should be recognised.");

        // The header must never carry secrets or PII.
        Assert.That(
            payload.EnumerateObject().Select(p => p.Name),
            Is.EquivalentTo(new[] { "name", "version", "env" })
        );
    }

    [Test]
    public void ClientOptions_DefaultConstructor_SeedsTelemetryHeader()
    {
        var options = new ClientOptions();

        Assert.That(options.Headers.ContainsKey(HeaderName), Is.True);
    }

    [Test]
    public async SystemTask ClientOptions_Clone_PreservesTelemetryHeader()
    {
        // MyAccountApiClient clones options when injecting the auth header - the clone must keep it.
        var clone = new ClientOptions().Clone();

        Assert.That(clone.Headers.ContainsKey(HeaderName), Is.True);
        var value = await clone.Headers[HeaderName].ResolveAsync().ConfigureAwait(false);
        AssertWellFormed(value);
    }

    [Test]
    public async SystemTask LowLevelClient_SendsTelemetryHeader()
    {
        StubFactors();

        var client = new MyAccountApiClient(
            "TOKEN",
            new ClientOptions { BaseUrl = _server.Urls[0], MaxRetries = 0 }
        );

        await client.Factors.ListAsync();

        AssertWellFormed(SingleTelemetryHeader());
    }

    [Test]
    public async SystemTask LowLevelClient_WithNoClientOptions_SendsTelemetryHeader()
    {
        StubFactors();

        // No ClientOptions supplied: the client defaults one internally, which must still be seeded.
        var client = new MyAccountApiClient("TOKEN");

        await client.Factors.ListAsync(
            new RequestOptions { BaseUrl = _server.Urls[0], MaxRetries = 0 }
        );

        AssertWellFormed(SingleTelemetryHeader());
    }

    [Test]
    public async SystemTask WrapperClient_SendsTelemetryHeader()
    {
        StubFactors();

        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                MaxRetries = 0,
            }
        );

        await client.Factors.ListAsync();

        AssertWellFormed(SingleTelemetryHeader());
    }

    [Test]
    public async SystemTask WrapperClient_WithCustomHttpClient_SendsTelemetryHeader()
    {
        StubFactors();

        using var httpClient = new HttpClient();
        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                HttpClient = httpClient,
                MaxRetries = 0,
            }
        );

        await client.Factors.ListAsync();

        AssertWellFormed(SingleTelemetryHeader());
    }

    [Test]
    public async SystemTask WrapperClient_WithAdditionalHeaders_StillSendsTelemetryHeader()
    {
        StubFactors();

        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                MaxRetries = 0,
                AdditionalHeaders = new Dictionary<string, string?> { { "X-Custom", "value" } },
            }
        );

        await client.Factors.ListAsync();

        var entry = _server.LogEntries.Single();
        Assert.That(entry.RequestMessage.Headers!["X-Custom"].First(), Is.EqualTo("value"));
        AssertWellFormed(entry.RequestMessage.Headers![HeaderName].First());
    }

    [Test]
    public async SystemTask PerRequestOptions_DoNotStripTelemetryHeader()
    {
        StubFactors();

        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                MaxRetries = 0,
            }
        );

        await client.Factors.ListAsync(
            new RequestOptions
            {
                AdditionalHeaders = new Dictionary<string, string?> { { "X-Request", "scoped" } },
                Timeout = TimeSpan.FromSeconds(10),
            }
        );

        var entry = _server.LogEntries.Single();
        Assert.That(entry.RequestMessage.Headers!["X-Request"].First(), Is.EqualTo("scoped"));
        AssertWellFormed(entry.RequestMessage.Headers![HeaderName].First());
    }

    [Test]
    public async SystemTask CallerSuppliedTelemetryHeader_TakesPrecedence()
    {
        StubFactors();

        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                MaxRetries = 0,
                AdditionalHeaders = new Dictionary<string, string?>
                {
                    { HeaderName, "caller-supplied" },
                },
            }
        );

        await client.Factors.ListAsync();

        // AdditionalHeaders are applied after Options.Headers, so a caller can still override.
        Assert.That(SingleTelemetryHeader(), Is.EqualTo("caller-supplied"));
    }

    [Test]
    public async SystemTask RetriedRequests_AllCarryTelemetryHeader()
    {
        _server
            .Given(WireMockRequest.Create().WithPath("/factors").UsingGet())
            .InScenario("Retry")
            .WillSetStateTo("Failed")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500));

        _server
            .Given(WireMockRequest.Create().WithPath("/factors").UsingGet())
            .InScenario("Retry")
            .WhenStateIs("Failed")
            .RespondWith(WireMockResponse.Create().WithStatusCode(200).WithBody(FactorsResponse));

        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                MaxRetries = 2,
            }
        );

        await client.Factors.ListAsync();

        var entries = _server.LogEntries.ToList();
        Assert.That(entries, Has.Count.GreaterThan(1), "Expected at least one retry.");
        foreach (var entry in entries)
        {
            AssertWellFormed(entry.RequestMessage.Headers![HeaderName].First());
        }
    }

    [Test]
    public async SystemTask PaginatedRequests_AllCarryTelemetryHeader()
    {
        // First page advertises a cursor, second page terminates the sequence.
        _server
            .Given(
                WireMockRequest
                    .Create()
                    .WithPath("/connected-accounts/connections")
                    .WithParam("from", "cursor-2")
                    .UsingGet()
            )
            .RespondWith(
                WireMockResponse
                    .Create()
                    .WithStatusCode(200)
                    .WithBody(
                        """
                        { "connections": [ { "name": "b", "strategy": "s", "scopes": [] } ] }
                        """
                    )
            );

        _server
            .Given(
                WireMockRequest.Create().WithPath("/connected-accounts/connections").UsingGet()
            )
            .RespondWith(
                WireMockResponse
                    .Create()
                    .WithStatusCode(200)
                    .WithBody(
                        """
                        {
                          "connections": [ { "name": "a", "strategy": "s", "scopes": [] } ],
                          "next": "cursor-2"
                        }
                        """
                    )
            );

        using var client = new MyAccountClient(
            new MyAccountClientOptions
            {
                TokenProvider = new DelegateTokenProvider(_ => SystemTask.FromResult("token")),
                BaseUrl = _server.Urls[0],
                MaxRetries = 0,
            }
        );

        var pager = await client.ConnectedAccounts.Connections.ListAsync(
            new ListConnectedAccountsConnectionsRequestParameters()
        );

        var names = new List<string?>();
        await foreach (var connection in pager)
        {
            names.Add(connection.Name);
        }

        var entries = _server.LogEntries.ToList();
        Assert.That(names, Has.Count.EqualTo(2), "Expected the pager to fetch both pages.");
        Assert.That(entries, Has.Count.EqualTo(2), "Expected one request per page.");
        foreach (var entry in entries)
        {
            AssertWellFormed(entry.RequestMessage.Headers![HeaderName].First());
        }
    }
}
