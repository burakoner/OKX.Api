namespace OKX.Api.Trade;

internal class OkxTradeOrderSideConverter(bool quotes) : BaseConverter<OkxTradeOrderSide>(quotes)
{
    public OkxTradeOrderSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOrderSide, string>> Mapping =>
    [
        new(OkxTradeOrderSide.Buy, "buy"),
        new(OkxTradeOrderSide.Sell, "sell"),
    ];
}