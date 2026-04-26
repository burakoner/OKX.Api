using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicMarkPriceContractTests
{
    [Fact]
    public void ManualEventMarkPriceFixture_MapsEmptyMarkPriceToNull()
    {
        var response = DeserializeManual("Public", "get-mark-price-events-empty-string.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(OkxInstrumentType.Events, item.InstrumentType);
        Assert.Equal("BTC-ABOVE-DAILY-260504-1600-78000", item.InstrumentId);
        Assert.Null(item.MarkPrice);
    }

    [Fact]
    public void LiveProductionEventMarkPriceFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-mark-price-events-btc-above-daily.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Events, x.InstrumentType));
    }

    private static OkxRestApiResponse<List<OkxPublicMarkPrice>> DeserializeManual(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicMarkPrice>> DeserializeLive(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadLive(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicMarkPrice>> Deserialize(string json)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicMarkPrice>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
