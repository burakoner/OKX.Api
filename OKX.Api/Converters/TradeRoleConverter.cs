namespace OKX.Api.Converters;

internal class TradeRoleConverter : BaseConverter<OkxTradeRole>
{
    public TradeRoleConverter() : this(true) { }
    public TradeRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTradeRole, string>> Mapping => new List<KeyValuePair<OkxTradeRole, string>>
    {
        new KeyValuePair<OkxTradeRole, string>(OkxTradeRole.Taker, "T"),
        new KeyValuePair<OkxTradeRole, string>(OkxTradeRole.Maker, "M"),
    };
}