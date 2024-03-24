using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridBackTestingDurationConverter : BaseConverter<OkxGridBackTestingDuration>
{
    public OkxGridBackTestingDurationConverter() : this(true) { }
    public OkxGridBackTestingDurationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridBackTestingDuration, string>> Mapping =>
    [
        new(OkxGridBackTestingDuration.OneWeek, "7D"),
        new(OkxGridBackTestingDuration.OneMonth, "30D"),
        new(OkxGridBackTestingDuration.SixMonths, "180D"),
    ];
}