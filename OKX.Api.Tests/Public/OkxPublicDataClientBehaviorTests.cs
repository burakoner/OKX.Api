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
        Assert.Single(result.Data);
        var request = Assert.Single(server.Requests);
        Assert.Contains("instType=SWAP", request.Query);
        Assert.Contains("instFamily=BTC-USD", request.Query);
        Assert.DoesNotContain("ccy=", request.Query);
    }

    [Fact]
    public async Task GetInsuranceFundsAsync_SerializesMarginCurrencyWithoutInstrumentFamily()
    {
        using var server = CreateServer("/api/v5/public/insurance-fund", "Public", "get-insurance-fund-security-fund.json");
        var client = CreateSignedClient(server);

        var result = await client.Public.GetInsuranceFundsAsync(OkxInstrumentType.Margin, currency: "BTC", limit: 1);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("instType=MARGIN", request.Query);
        Assert.Contains("ccy=BTC", request.Query);
        Assert.DoesNotContain("instFamily=", request.Query);
    }

    [Theory]
    [InlineData(OkxInstrumentType.Swap, null, null)]
    [InlineData(OkxInstrumentType.Swap, "BTC-USD", "BTC")]
    [InlineData(OkxInstrumentType.Margin, null, null)]
    [InlineData(OkxInstrumentType.Margin, "BTC-USD", "BTC")]
    public async Task GetInsuranceFundsAsync_RejectsInvalidConditionalFilters(
        OkxInstrumentType instrumentType,
        string? instrumentFamily,
        string? currency)
    {
        using var server = CreateServer("/api/v5/public/insurance-fund", "Public", "get-insurance-fund-security-fund.json");
        var client = CreateSignedClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetInsuranceFundsAsync(
            instrumentType,
            instrumentFamily: instrumentFamily,
            currency: currency));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GetInsuranceFundsAsync_RejectsUnknownTypeBeforeSending()
    {
        using var server = CreateServer("/api/v5/public/insurance-fund", "Public", "get-insurance-fund-security-fund.json");
        var client = CreateSignedClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetInsuranceFundsAsync(
            OkxInstrumentType.Margin,
            (OkxPublicInsuranceType)byte.MaxValue,
            currency: "BTC"));

        Assert.Empty(server.Requests);
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
