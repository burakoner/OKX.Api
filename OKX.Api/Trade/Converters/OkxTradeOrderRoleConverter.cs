namespace OKX.Api.Trade;

internal class OkxTradeOrderRoleConverter(bool quotes) : BaseConverter<OkxTradeOrderRole>(quotes)
{
    public OkxTradeOrderRoleConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOrderRole, string>> Mapping =>
    [
        new(OkxTradeOrderRole.Taker, "T"),
        new(OkxTradeOrderRole.Maker, "M"),
    ];
}