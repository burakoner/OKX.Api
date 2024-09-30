namespace OKX.Api.Public;

/// <summary>
/// OKX Trade History Pagination Type
/// </summary>
public enum OkxPublicTradeHistoryPaginationType
{
    /// <summary>
    /// TradeId
    /// </summary>
    [Map("1")]
    TradeId,

    /// <summary>
    /// Timestamp
    /// </summary>
    [Map("2")]
    Timestamp,
}