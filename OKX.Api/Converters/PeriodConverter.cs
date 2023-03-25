namespace OKX.Api.Converters;

internal class PeriodConverter : BaseConverter<OkxPeriod>
{
    public PeriodConverter() : this(true) { }
    public PeriodConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPeriod, string>> Mapping => new List<KeyValuePair<OkxPeriod, string>>
    {
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.OneMinute, "1m"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.ThreeMinutes, "3m"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.FiveMinutes, "5m"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.FifteenMinutes, "15m"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.ThirtyMinutes, "30m"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.OneHour, "1H"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.TwoHours, "2H"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.FourHours, "4H"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.SixHours, "6H"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.TwelveHours, "12H"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.OneDay, "1D"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.OneWeek, "1W"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.OneMonth, "1M"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.ThreeMonths, "3M"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.SixMonths, "6M"),
        new KeyValuePair<OkxPeriod, string>(OkxPeriod.OneYear, "1Y"),
    };
}