namespace OKX.Api.Trade;

internal class OkxTradePositionSideConverter(bool quotes) : BaseConverter<OkxTradePositionSide>(quotes)
{
    public OkxTradePositionSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradePositionSide, string>> Mapping =>
    [
        new(OkxTradePositionSide.Long, "long"),
        new(OkxTradePositionSide.Short, "short"),
        new(OkxTradePositionSide.Net, "net"),
    ];
}