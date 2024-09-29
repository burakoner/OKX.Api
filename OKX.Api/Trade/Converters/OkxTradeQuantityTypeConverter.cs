namespace OKX.Api.Trade;

internal class OkxTradeQuantityTypeConverter(bool quotes) : BaseConverter<OkxTradeQuantityType>(quotes)
{
    public OkxTradeQuantityTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeQuantityType, string>> Mapping =>
    [
        new(OkxTradeQuantityType.BaseCurrency, "base_ccy"),
        new(OkxTradeQuantityType.QuoteCurrency, "quote_ccy"),
    ];
}