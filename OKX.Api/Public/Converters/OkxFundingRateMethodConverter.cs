using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxFundingRateMethodConverter(bool quotes) : BaseConverter<OkxFundingRateMethod>(quotes)
{
    public OkxFundingRateMethodConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingRateMethod, string>> Mapping =>
    [
        new(OkxFundingRateMethod.CurrentPeriod, "current_period"),
        new(OkxFundingRateMethod.NextPeriod, "next_period"),
    ];
}