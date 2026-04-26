using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicInstrumentContractTests
{
    [Fact]
    public void ManualPrivateInstrumentsFixture_ParsesQuotaFields()
    {
        var response = DeserializeManual("Account", "get-instruments-private-with-quotas.json");

        var instrument = Assert.Single(response.Data!);
        Assert.Equal("BTC-USDT-SWAP", instrument.InstrumentId);
        Assert.Equal(OkxPublicInstrumentCategory.Crypto, instrument.InstrumentCategory);
        Assert.Equal(1250.50m, instrument.LongPositionRemainingQuota);
        Assert.Equal(775.25m, instrument.ShortPositionRemainingQuota);
    }

    [Fact]
    public void ManualPublicInstrumentsFixture_ParsesDocumentedInstCategoryMappings()
    {
        var response = DeserializeManual("Public", "get-instruments-instcategory-values.json");

        var instruments = response.Data!.ToDictionary(x => x.InstrumentId);
        Assert.Equal(OkxPublicInstrumentCategory.Crypto, instruments["BTC-USDT"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Stocks, instruments["AAPL-USDT"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Commodities, instruments["XAU-USDT-SWAP"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Forex, instruments["EUR-USDT"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Bonds, instruments["US10Y-USDT"].InstrumentCategory);
    }

    [Fact]
    public void ManualPublicInstrumentsFixture_MapsEmptyInstCategoryToNull()
    {
        var response = DeserializeManual("Public", "get-instruments-instcategory-values.json");

        var unknownInstrument = response.Data!.Single(x => x.InstrumentId == "UNKNOWN-USDT");
        Assert.Null(unknownInstrument.InstrumentCategory);
    }

    [Fact]
    public void LiveProductionSpotInstrumentsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-instruments-spot.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentId == "BTC-USDT");
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Spot, x.InstrumentType));
    }

    [Fact]
    public void LiveProductionMarginInstrumentsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-instruments-margin.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentId == "BTC-USDT");
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Margin, x.InstrumentType));
    }

    [Fact]
    public void LiveProductionSwapInstrumentsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-instruments-swap.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentId == "BTC-USDT-SWAP");
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Swap, x.InstrumentType));
    }

    [Fact]
    public void LiveProductionFuturesInstrumentsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-instruments-futures.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Futures, x.InstrumentType));
    }

    [Fact]
    public void LiveProductionOptionInstrumentsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-instruments-option-btc-usd.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Option, x.InstrumentType));
    }

    [Fact]
    public void LiveProductionEventInstrumentsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Public", "get-instruments-events-btc-above-daily.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Events, x.InstrumentType));
    }

    [Fact]
    public void LiveDemoSpotInstrumentsFixture_Uses64BitInstIdCodes()
    {
        var response = DeserializeLive("Demo", "Public", "get-instruments-spot.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentId == "BTC-USDT");
        Assert.Contains(response.Data!, x => x.InstrumentIdCode > int.MaxValue);
    }

    [Fact]
    public void LiveDemoMarginInstrumentsFixture_Uses64BitInstIdCodes()
    {
        var response = DeserializeLive("Demo", "Public", "get-instruments-margin.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentId == "BTC-USDT");
        Assert.Contains(response.Data!, x => x.InstrumentIdCode > int.MaxValue);
    }

    [Fact]
    public void LiveDemoSwapInstrumentsFixture_Uses64BitInstIdCodes()
    {
        var response = DeserializeLive("Demo", "Public", "get-instruments-swap.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentId == "BTC-USDT-SWAP");
        Assert.Contains(response.Data!, x => x.InstrumentIdCode > int.MaxValue);
    }

    [Fact]
    public void LiveDemoFuturesInstrumentsFixture_Uses64BitInstIdCodes()
    {
        var response = DeserializeLive("Demo", "Public", "get-instruments-futures.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.InstrumentIdCode > int.MaxValue);
    }

    [Fact]
    public void LiveDemoOptionInstrumentsFixture_Uses64BitInstIdCodes()
    {
        var response = DeserializeLive("Demo", "Public", "get-instruments-option-btc-usd.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Option, x.InstrumentType));
        Assert.Contains(response.Data!, x => x.InstrumentIdCode > int.MaxValue);
    }

    [Fact]
    public void LiveDemoEventInstrumentsFixture_Uses64BitInstIdCodes()
    {
        var response = DeserializeLive("Demo", "Public", "get-instruments-events-btc-above-daily.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Events, x.InstrumentType));
        Assert.Contains(response.Data!, x => x.InstrumentIdCode > int.MaxValue);
    }

    private static OkxRestApiResponse<List<OkxPublicInstrument>> DeserializeManual(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicInstrument>> DeserializeLive(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadLive(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicInstrument>> Deserialize(string json)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicInstrument>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
