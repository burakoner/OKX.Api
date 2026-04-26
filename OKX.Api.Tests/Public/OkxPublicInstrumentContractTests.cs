using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicInstrumentContractTests
{
    [Fact]
    public void PrivateInstrumentsFixture_ParsesQuotaFields()
    {
        var response = Deserialize("Account", "get-instruments-private-with-quotas.json");

        var instrument = Assert.Single(response.Data!);
        Assert.Equal("BTC-USDT-SWAP", instrument.InstrumentId);
        Assert.Equal(OkxPublicInstrumentCategory.Crypto, instrument.InstrumentCategory);
        Assert.Equal(1250.50m, instrument.LongPositionRemainingQuota);
        Assert.Equal(775.25m, instrument.ShortPositionRemainingQuota);
    }

    [Fact]
    public void PublicInstrumentsFixture_ParsesDocumentedInstCategoryMappings()
    {
        var response = Deserialize("Public", "get-instruments-instcategory-values.json");

        var instruments = response.Data!.ToDictionary(x => x.InstrumentId);
        Assert.Equal(OkxPublicInstrumentCategory.Crypto, instruments["BTC-USDT"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Stocks, instruments["AAPL-USDT"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Commodities, instruments["XAU-USDT-SWAP"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Forex, instruments["EUR-USDT"].InstrumentCategory);
        Assert.Equal(OkxPublicInstrumentCategory.Bonds, instruments["US10Y-USDT"].InstrumentCategory);
    }

    [Fact]
    public void PublicInstrumentsFixture_MapsEmptyInstCategoryToNull()
    {
        var response = Deserialize("Public", "get-instruments-instcategory-values.json");

        var unknownInstrument = response.Data!.Single(x => x.InstrumentId == "UNKNOWN-USDT");
        Assert.Null(unknownInstrument.InstrumentCategory);
    }

    private static OkxRestApiResponse<List<OkxPublicInstrument>> Deserialize(params string[] fixturePath)
    {
        var json = FixtureReader.Read(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicInstrument>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
