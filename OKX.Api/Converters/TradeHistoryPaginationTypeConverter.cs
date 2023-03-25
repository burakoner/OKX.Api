namespace OKX.Api.Converters;

internal class TradeHistoryPaginationTypeConverter : BaseConverter<OkxTradeHistoryPaginationType>
{
    public TradeHistoryPaginationTypeConverter() : this(true) { }
    public TradeHistoryPaginationTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTradeHistoryPaginationType, string>> Mapping => new List<KeyValuePair<OkxTradeHistoryPaginationType, string>>
    {
        new KeyValuePair<OkxTradeHistoryPaginationType, string>(OkxTradeHistoryPaginationType.TradeId, "1"),
        new KeyValuePair<OkxTradeHistoryPaginationType, string>(OkxTradeHistoryPaginationType.Timestamp, "2"),
    };
}