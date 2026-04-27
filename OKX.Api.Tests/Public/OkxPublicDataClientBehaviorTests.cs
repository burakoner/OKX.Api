using Newtonsoft.Json.Linq;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicDataClientBehaviorTests
{
    [Fact]
    public async Task GetInsuranceFundsAsync_ReturnsFullDocumentedArrayResponse()
    {
        using var server = CreateServer("/api/v5/public/insurance-fund", "Public", "get-insurance-fund-security-fund.json");
        var client = CreateSignedClient(server);

        var result = await client.Public.GetInsuranceFundsAsync(OkxInstrumentType.Swap, instrumentFamily: "BTC-USD", limit: 2);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public async Task GetEconomicCalendarDataAsync_SendsSignedRequest()
    {
        using var server = CreateServer("/api/v5/public/economic-calendar", "Public", "get-economic-calendar-date-span.json");
        var client = CreateSignedClient(server);

        var result = await client.Public.GetEconomicCalendarDataAsync("united_states", OkxPublicEventImportance.High, limit: 2);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("region=united_states", request.Query);
        Assert.Contains("importance=3", request.Query);
        Assert.Equal("key", request.Headers["OK-ACCESS-KEY"]);
        Assert.True(request.Headers.ContainsKey("OK-ACCESS-SIGN"));
        Assert.True(request.Headers.ContainsKey("OK-ACCESS-TIMESTAMP"));
        Assert.True(request.Headers.ContainsKey("OK-ACCESS-PASSPHRASE"));
    }

    private static LocalOkxRestServer CreateServer(string path, params string[] fixturePath)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual(fixturePath),
        });

    private static OkxRestApiClient CreateSignedClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }
}
