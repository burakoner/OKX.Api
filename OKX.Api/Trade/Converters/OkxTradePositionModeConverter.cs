namespace OKX.Api.Trade;

internal class OkxTradePositionModeConverter(bool quotes) : BaseConverter<OkxTradePositionMode>(quotes)
{
    public OkxTradePositionModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradePositionMode, string>> Mapping =>
    [
        new(OkxTradePositionMode.LongShortMode, "long_short_mode"),
        new(OkxTradePositionMode.NetMode, "net_mode"),
    ];
}