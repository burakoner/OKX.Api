using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicEstimatedPriceContractTests
{
    [Fact]
    public void ManualEstimatedPriceFixture_ParsesRestResponse()
    {
        var response = DeserializeRest<OkxPublicEstimatedPrice>("Public", "get-estimated-price.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(OkxInstrumentType.Futures, item.InstrumentType);
        Assert.Equal("BTC-USDT-201227", item.InstrumentId);
        Assert.Equal(200m, item.EstimatedPrice);
        Assert.Null(item.SettlementType);
        Assert.Equal(1597026383085L, item.Timestamp);
    }

    [Fact]
    public void ManualEstimatedSettlementInfoFixture_ParsesRestResponse()
    {
        var response = DeserializeRest<OkxPublicEstimatedSettlementInfo>("Public", "get-estimated-settlement-info.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("XRP-USDT-250307", item.InstrumentId);
        Assert.Equal(2.5666068562369959m, item.EstimatedSettlementPrice);
        Assert.Equal(1741248000000L, item.NextSettlementTimestamp);
        Assert.Equal(1741246429748L, item.Timestamp);
    }

    [Fact]
    public void ManualEstimatedPriceSocketFixture_ParsesSettlementType()
    {
        var items = DeserializeSocketData<OkxPublicEstimatedPrice>("Public", "ws-estimated-price.json");

        var item = Assert.Single(items);
        Assert.Equal(OkxInstrumentType.Futures, item.InstrumentType);
        Assert.Equal("XRP-USDT-250307", item.InstrumentId);
        Assert.Equal(2.4230631578947368m, item.EstimatedPrice);
        Assert.Equal(OkxPublicSettlementType.FuturesSettlement, item.SettlementType);
        Assert.Equal(1741244598708L, item.Timestamp);
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static List<T> DeserializeSocketData<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var token = JObject.Parse(json)["data"];

        Assert.NotNull(token);

        var response = token!.ToObject<List<T>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }
}
