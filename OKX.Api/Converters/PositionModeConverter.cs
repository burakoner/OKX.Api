namespace OKX.Api.Converters;

internal class PositionModeConverter : BaseConverter<OkxPositionMode>
{
    public PositionModeConverter() : this(true) { }
    public PositionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPositionMode, string>> Mapping => new List<KeyValuePair<OkxPositionMode, string>>
    {
        new KeyValuePair<OkxPositionMode, string>(OkxPositionMode.LongShortMode, "long_short_mode"),
        new KeyValuePair<OkxPositionMode, string>(OkxPositionMode.NetMode, "net_mode"),
    };
}