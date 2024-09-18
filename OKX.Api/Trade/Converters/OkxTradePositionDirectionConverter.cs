using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradePositionDirectionConverter(bool quotes) : BaseConverter<OkxTradePositionDirection>(quotes)
{
    public OkxTradePositionDirectionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradePositionDirection, string>> Mapping =>
    [
        new(OkxTradePositionDirection.Long, "long"),
        new(OkxTradePositionDirection.Short, "short"),
    ];
}