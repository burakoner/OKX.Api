using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxTradeHistoryPaginationTypeConverter(bool quotes) : BaseConverter<OkxTradeHistoryPaginationType>(quotes)
{
    public OkxTradeHistoryPaginationTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeHistoryPaginationType, string>> Mapping =>
    [
        new(OkxTradeHistoryPaginationType.TradeId, "1"),
        new(OkxTradeHistoryPaginationType.Timestamp, "2"),
    ];
}