using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradeOrderSideConverter(bool quotes) : BaseConverter<OkxTradeOrderSide>(quotes)
{
    public OkxTradeOrderSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOrderSide, string>> Mapping =>
    [
        new(OkxTradeOrderSide.Buy, "buy"),
        new(OkxTradeOrderSide.Sell, "sell"),
    ];
}