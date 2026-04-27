using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicDataContractTests
{
    [Fact]
    public void ManualDiscountInfoFixture_ParsesInfinityAndDeprecatedFields()
    {
        var response = DeserializeRest<OkxPublicDiscountInfo>("Public", "get-discount-rate-interest-free-quota-infinity.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(OkxPublicCollateralRestrictionStatus.Disabled, item.CollateralRestrictionStatus);
#pragma warning disable CS0612
        Assert.False(item.CollateralRestricted);
        Assert.Equal("1", item.DiscountLevel);
#pragma warning restore CS0612
        var detail = Assert.Single(item.Details);
        Assert.True(detail.MaximumAmountIsPositiveInfinity);
        Assert.Null(detail.MaximumAmount);
    }

    [Fact]
    public void ManualInterestRateLoanQuotaFixture_ParsesStrategyTypeAndDeprecatedDiscounts()
    {
        var response = DeserializeSingleFromList<OkxPublicInterestRateLoanQuota>("Public", "get-interest-rate-loan-quota-stgytype.json");

        Assert.Equal(OkxAccountStrategyType.General, response.Configurations[0].StrategyType);
        Assert.Equal(OkxAccountStrategyType.DeltaNeutral, response.Configurations[1].StrategyType);
#pragma warning disable CS0612
        Assert.Null(response.VIP[0].InterestRateDiscount);
        Assert.Null(response.Regular[0].InterestRateDiscount);
#pragma warning restore CS0612
    }

    [Fact]
    public void ManualInsuranceFundFixture_ParsesRegularUpdateAndAdlEntries()
    {
        var response = DeserializeRest<OkxPublicInsuranceFund>("Public", "get-insurance-fund-security-fund.json");

        Assert.Equal(2, response.Data!.Count);

        var regularUpdate = response.Data.Single(x => x.InstrumentFamily == "ETH-USD");
        var regularDetail = Assert.Single(regularUpdate.Details);
        Assert.Equal(OkxPublicInsuranceFundDetailType.RegularUpdate, regularDetail.Type);
        Assert.Null(regularDetail.Amount);
        Assert.Null(regularDetail.MaximumBalance);
        Assert.Null(regularDetail.MaximumBalanceTimestamp);

        var adl = response.Data.Single(x => x.InstrumentFamily == "BTC-USD");
        var adlDetail = Assert.Single(adl.Details);
        Assert.Equal(OkxPublicInsuranceFundDetailType.Adl, adlDetail.Type);
        Assert.Equal(4000m, adlDetail.MaximumBalance);
        Assert.Equal(1777283890000L, adlDetail.MaximumBalanceTimestamp);
#pragma warning disable CS0612
        Assert.Equal(0.035m, adlDetail.DeclineRate);
#pragma warning restore CS0612
    }

    [Fact]
    public void ManualEconomicCalendarFixture_MapsDateSpanValues()
    {
        var response = DeserializeRest<OkxPublicEconomicCalendarEvent>("Public", "get-economic-calendar-date-span.json");
        var items = response.Data!;

        var dated = items.Single(x => x.CalendarId == 330631);
        Assert.True(dated.HasExactTime);

        var dateOnly = items.Single(x => x.CalendarId == 330632);
        Assert.False(dateOnly.HasExactTime);
    }

    [Fact]
    public void ManualEconomicCalendarFixture_AllowsEmptyReferenceDate()
    {
        var response = DeserializeRest<OkxPublicEconomicCalendarEvent>("Public", "get-economic-calendar-empty-refdate.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(419900L, item.CalendarId);
        Assert.Null(item.ReferenceDatestamp);
        Assert.Null(item.ReferenceDate);
    }

    [Fact]
    public void ManualOpenInterestFixture_ParsesFractionalOi()
    {
        var response = DeserializeRest<OkxPublicOpenInterest>("Public", "get-open-interest-fractional.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(3299312.06000002104m, item.OpenInterestCount);
        Assert.Equal(32993.1206000002104m, item.OpenInterestCoin);
    }

    [Fact]
    public void LiveDiscountInfoFixture_ParsesCurrentBtcSnapshot()
    {
        var response = DeserializeRest<OkxPublicDiscountInfo>(FixtureReader.ReadLive("Production", "Public", "get-discount-rate-interest-free-quota-btc.json"));

        var item = Assert.Single(response.Data!);
        Assert.Equal("BTC", item.Currency);
        Assert.NotEmpty(item.Details);
    }

    [Fact]
    public void LiveInterestRateLoanQuotaFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeSingleFromList<OkxPublicInterestRateLoanQuota>(FixtureReader.ReadLive("Production", "Public", "get-interest-rate-loan-quota.json"));

        Assert.NotEmpty(response.Basic);
        Assert.NotEmpty(response.Configurations);
        Assert.Contains(response.Configurations, x => x.StrategyType == OkxAccountStrategyType.General);
        Assert.Contains(response.Configurations, x => x.StrategyType == OkxAccountStrategyType.DeltaNeutral);
    }

    [Fact]
    public void LiveInsuranceFundFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeRest<OkxPublicInsuranceFund>(FixtureReader.ReadLive("Production", "Public", "get-insurance-fund-swap-btc-usd.json"));

        var item = Assert.Single(response.Data!);
        Assert.Equal(OkxInstrumentType.Swap, item.InstrumentType);
        Assert.NotEmpty(item.Details);
        Assert.All(item.Details, detail => Assert.Equal(OkxPublicInsuranceFundDetailType.RegularUpdate, detail.Type));
    }

    [Fact]
    public void LiveUnderlyingFixture_ParsesNestedArraySnapshot()
    {
        var response = DeserializeUnderlying(FixtureReader.ReadLive("Production", "Public", "get-underlying-futures.json"));

        Assert.NotEmpty(response);
        Assert.Contains("BTC-USDT", response);
    }

    [Fact]
    public void LiveOptionTickBandsFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeRest<OkxPublicOptionTickBands>(FixtureReader.ReadLive("Production", "Public", "get-option-tick-bands-option.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal(OkxInstrumentType.Option, x.InstrumentType));
        Assert.All(response.Data!, x => Assert.NotEmpty(x.TickBands));
    }

    [Fact]
    public void LiveOpenInterestFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeRest<OkxPublicOpenInterest>(FixtureReader.ReadLive("Production", "Public", "get-open-interest-btc-usdt-swap.json"));

        var item = Assert.Single(response.Data!);
        Assert.Equal("BTC-USDT-SWAP", item.InstrumentId);
        Assert.True(item.OpenInterestCount > 0m);
    }

    [Fact]
    public void LiveIndexComponentsFixture_ParsesCurrentSnapshot()
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<OkxPublicIndexComponents>>(FixtureReader.ReadLive("Production", "Public", "get-index-components-btc-usd.json"), SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        Assert.Equal("BTC-USD", response.Data.Index);
        Assert.NotEmpty(response.Data.Components);
    }

    [Fact]
    public void LiveEconomicCalendarFixture_ParsesAuthenticatedProductionSnapshot()
    {
        var response = DeserializeRest<OkxPublicEconomicCalendarEvent>(FixtureReader.ReadLive("Production", "Public", "get-economic-calendar-united-states.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal("United States", x.Region));
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath) where T : class
        => DeserializeRest<T>(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(string json) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static T DeserializeSingleFromList<T>(params string[] fixturePath) where T : class
        => DeserializeSingleFromList<T>(FixtureReader.ReadManual(fixturePath));

    private static T DeserializeSingleFromList<T>(string json) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return Assert.Single(response.Data);
    }

    private static List<string> DeserializeUnderlying(string json)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<List<string>>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return Assert.Single(response.Data);
    }
}
