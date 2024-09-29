namespace OKX.Api.Trade;

internal class OkxTradeDebtTypeConverter(bool quotes) : BaseConverter<OkxTradeDebtType>(quotes)
{
    public OkxTradeDebtTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeDebtType, string>> Mapping =>
    [
        new(OkxTradeDebtType.Cross, "cross"),
        new(OkxTradeDebtType.Isolated, "isolated"),
    ];
}