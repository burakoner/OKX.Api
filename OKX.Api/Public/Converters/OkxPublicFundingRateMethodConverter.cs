namespace OKX.Api.Public;

internal class OkxPublicFundingRateMethodConverter(bool quotes) : BaseConverter<OkxPublicFundingRateMethod>(quotes)
{
    public OkxPublicFundingRateMethodConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicFundingRateMethod, string>> Mapping =>
    [
        new(OkxPublicFundingRateMethod.CurrentPeriod, "current_period"),
        new(OkxPublicFundingRateMethod.NextPeriod, "next_period"),
    ];
}