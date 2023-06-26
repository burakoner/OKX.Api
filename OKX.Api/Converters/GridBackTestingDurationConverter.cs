namespace OKX.Api.Converters;

internal class GridBackTestingDurationConverter : BaseConverter<OkxGridBackTestingDuration>
{
    public GridBackTestingDurationConverter() : this(true) { }
    public GridBackTestingDurationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridBackTestingDuration, string>> Mapping => new List<KeyValuePair<OkxGridBackTestingDuration, string>>
    {
        new KeyValuePair<OkxGridBackTestingDuration, string>(OkxGridBackTestingDuration.OneWeek, "7D"),
        new KeyValuePair<OkxGridBackTestingDuration, string>(OkxGridBackTestingDuration.OneMonth, "30D"),
        new KeyValuePair<OkxGridBackTestingDuration, string>(OkxGridBackTestingDuration.SixMonths, "180D"),
    };
}