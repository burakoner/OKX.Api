namespace OKX.Api.Trade;

internal class OkxTradePositionDirectionConverter(bool quotes) : BaseConverter<OkxTradePositionDirection>(quotes)
{
    public OkxTradePositionDirectionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradePositionDirection, string>> Mapping =>
    [
        new(OkxTradePositionDirection.Long, "long"),
        new(OkxTradePositionDirection.Short, "short"),
    ];
}