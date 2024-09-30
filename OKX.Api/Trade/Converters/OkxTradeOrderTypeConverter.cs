namespace OKX.Api.Trade;

internal class OkxTradeOrderTypeConverter(bool quotes) : BaseConverter<OkxTradeOrderType>(quotes)
{
    public OkxTradeOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOrderType, string>> Mapping =>
    [
        new(OkxTradeOrderType.LimitOrder, "limit"),
        new(OkxTradeOrderType.MarketOrder, "market"),
        new(OkxTradeOrderType.PostOnly, "post_only"),
        new(OkxTradeOrderType.FillOrKill, "fok"),
        new(OkxTradeOrderType.ImmediateOrCancel, "ioc"),
        new(OkxTradeOrderType.OptimalLimitOrder, "optimal_limit_ioc"),
        new(OkxTradeOrderType.MarketMakerProtection, "mmp"),
        new(OkxTradeOrderType.MarektMakerProtectionAndPostOnly, "mmp_and_post_only"),
        new(OkxTradeOrderType.SimpleOptionsFillOrKill, "op_fok"),
    ];
}