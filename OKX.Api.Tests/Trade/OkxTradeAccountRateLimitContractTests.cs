using ApiSharp.Models;
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
