using ApiSharp.Models;
using ApiSharp.Throttling;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeAccountRateLimitContractTests
{
    [Fact]
    public void ManualAccountRateLimitFixture_ParsesEmptyVip4RateLimitFields()
    {
        var response = Deserialize("Trade", "get-account-rate-limit-empty-strings.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(1000, item.AccountRateLimit);
        Assert.Null(item.FillRatio);
        Assert.Null(item.MainFillRatio);
        Assert.Null(item.NextAccountRateLimit);
        Assert.Equal(1775625600000L, item.Timestamp);
    }

    [Fact]
    public async Task AccountRateLimitQuery_EnforcesOnePerSecondWithoutUpdatingGuardImplicitly()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/trade/account-rate-limit"] = FixtureReader.ReadManual("Trade", "get-account-rate-limit-empty-strings.json"),
        });
        var guard = new OkxTradeRateLimiter(1);
        var client = new OkxRestApiClient(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false, BaseAddress = server.BaseAddress, RateLimitingBehavior = RateLimitingBehavior.Fail,
            TradeRateLimiter = guard,
        });

        var result = await client.Trade.GetAccountRateLimitAsync();
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1000, result.Data.AccountRateLimit);
        Assert.IsType<ClientRateLimitError>((await client.Trade.GetAccountRateLimitAsync()).Error);
        Assert.Single(server.Requests);

        guard.RegisterInstrument(OkxTradeRateLimiterTests.Instrument("BTC-USDT-SWAP", 101), false);
        Assert.Null(guard.TryAcquire(OkxTradeRateLimitOperation.Place, [new("BTC-USDT-SWAP")], false, out var reservation));
        reservation!.Dispose();
        Assert.IsType<ClientRateLimitError>(guard.TryAcquire(OkxTradeRateLimitOperation.Amend, [new("BTC-USDT-SWAP")], false, out _));
        // Reading accRateLimit is not an instruction to raise the caller's lower cap of 1.
    }

    private static OkxRestApiResponse<List<OkxTradeAccountRateLimit>> Deserialize(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxTradeAccountRateLimit>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
