using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradePositionModeConverter(bool quotes) : BaseConverter<OkxTradePositionMode>(quotes)
{
    public OkxTradePositionModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradePositionMode, string>> Mapping =>
    [
        new(OkxTradePositionMode.LongShortMode, "long_short_mode"),
        new(OkxTradePositionMode.NetMode, "net_mode"),
    ];
}