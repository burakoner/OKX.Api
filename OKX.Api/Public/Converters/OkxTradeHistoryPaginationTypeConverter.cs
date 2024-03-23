using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxTradeHistoryPaginationTypeConverter : BaseConverter<OkxTradeHistoryPaginationType>
{
    public OkxTradeHistoryPaginationTypeConverter() : this(true) { }
    public OkxTradeHistoryPaginationTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTradeHistoryPaginationType, string>> Mapping =>
    [
        new(OkxTradeHistoryPaginationType.TradeId, "1"),
        new(OkxTradeHistoryPaginationType.Timestamp, "2"),
    ];
}