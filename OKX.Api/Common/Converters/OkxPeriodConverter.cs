using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxPeriodConverter(bool quotes) : BaseConverter<OkxPeriod>(quotes)
{
    public OkxPeriodConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPeriod, string>> Mapping =>
    [
        new(OkxPeriod.OneSecond, "1s"),
        new(OkxPeriod.OneMinute, "1m"),
        new(OkxPeriod.ThreeMinutes, "3m"),
        new(OkxPeriod.FiveMinutes, "5m"),
        new(OkxPeriod.FifteenMinutes, "15m"),
        new(OkxPeriod.ThirtyMinutes, "30m"),
        new(OkxPeriod.OneHour, "1H"),
        new(OkxPeriod.TwoHours, "2H"),
        new(OkxPeriod.FourHours, "4H"),
        new(OkxPeriod.SixHours, "6H"),
        new(OkxPeriod.TwelveHours, "12H"),
        new(OkxPeriod.OneDay, "1D"),
        new(OkxPeriod.OneWeek, "1W"),
        new(OkxPeriod.OneMonth, "1M"),
        new(OkxPeriod.ThreeMonths, "3M"),
        new(OkxPeriod.SixMonths, "6M"),
        new(OkxPeriod.OneYear, "1Y"),
    ];
}