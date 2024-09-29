namespace OKX.Api.Public;

internal class OkxPublicTradeHistoryPaginationTypeConverter(bool quotes) : BaseConverter<OkxPublicTradeHistoryPaginationType>(quotes)
{
    public OkxPublicTradeHistoryPaginationTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicTradeHistoryPaginationType, string>> Mapping =>
    [
        new(OkxPublicTradeHistoryPaginationType.TradeId, "1"),
        new(OkxPublicTradeHistoryPaginationType.Timestamp, "2"),
    ];
}