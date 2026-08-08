using ApiSharp.Converters;
using ApiSharp.Models;
using ApiSharp.Throttling;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicMarketMakerInstrumentTypeTests
{
    [Fact]
    public void ManualFixture_ParsesAllCurrentPairClassifications()
    {
        var response = Deserialize(FixtureReader.ReadManual("Public", "get-mm-instrument-types.json"));

        Assert.NotNull(response?.Data);
        Assert.Equal(3, response.Data.Count);
        Assert.Equal(OkxPublicMarketMakerPairType.HighLiquidity, response.Data[0].PairType);
        Assert.Equal(OkxPublicMarketMakerPairType.Crypto, response.Data[1].PairType);
        Assert.Equal(OkxPublicMarketMakerPairType.TraditionalFinance, response.Data[2].PairType);
        Assert.Equal(OkxInstrumentType.Spot, response.Data[1].InstrumentType);
    }

    [Fact]
    public void ProductionFixture_ParsesLiveTypePrefixedClassifications()
    {
        var response = Deserialize(FixtureReader.ReadLive("Production", "Public", "get-mm-instrument-types-sample.json"));

        Assert.NotNull(response.Data);
        Assert.Equal(OkxPublicMarketMakerPairType.HighLiquidity, response.Data[0].PairType);
        Assert.Equal(OkxPublicMarketMakerPairType.Crypto, response.Data[1].PairType);
        Assert.Equal(OkxPublicMarketMakerPairType.TraditionalFinance, response.Data[2].PairType);
        Assert.Equal("A", MapConverter.GetString(response.Data[0].PairType));
    }

    [Fact]
    public async Task GetMarketMakerInstrumentTypesAsync_SendsCurrentOptionalFiltersUnsigned()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Public.GetMarketMakerInstrumentTypesAsync(OkxInstrumentType.Swap, "AAOI-USDT-SWAP");

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(server.Requests);
        Assert.Contains("instType=SWAP", request.Query);
        Assert.Contains("instId=AAOI-USDT-SWAP", request.Query);
        Assert.False(request.Headers.ContainsKey("OK-ACCESS-KEY"));
    }

    [Fact]
    public async Task GetMarketMakerInstrumentTypesAsync_RejectsUnsupportedFiltersBeforeSending()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetMarketMakerInstrumentTypesAsync(OkxInstrumentType.Margin));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetMarketMakerInstrumentTypesAsync(instrumentId: " "));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GetMarketMakerInstrumentTypesAsync_EnforcesDocumentedIpLimit()
    {
        using var server = CreateServer();
        var client = CreateClient(server, RateLimitingBehavior.Fail);

        for (var i = 0; i < 5; i++)
            Assert.True((await client.Public.GetMarketMakerInstrumentTypesAsync()).Success);

        Assert.False((await client.Public.GetMarketMakerInstrumentTypesAsync()).Success);
        Assert.Equal(5, server.Requests.Count);
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["GET /api/v5/public/mm-instrument-types"] = FixtureReader.ReadManual("Public", "get-mm-instrument-types.json"),
        });

    private static OkxRestApiResponse<List<OkxPublicMarketMakerInstrumentType>> Deserialize(string json)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicMarketMakerInstrumentType>>>(json, SerializerOptions.WithConverters);
        Assert.NotNull(response?.Data);
        return response;
    }

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server, RateLimitingBehavior rateLimitingBehavior = RateLimitingBehavior.Wait)
    {
        var options = new OkxRestApiOptions
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
            RateLimitingBehavior = rateLimitingBehavior,
        };

        return new OkxRestApiClient(options);
    }
}