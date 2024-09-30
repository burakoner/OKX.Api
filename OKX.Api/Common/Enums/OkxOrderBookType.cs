namespace OKX.Api.Common;

/// <summary>
/// OKX Order Book Type
/// </summary>
public enum OkxOrderBookType
{
    /// <summary>
    /// OrderBook
    /// </summary>
    [Map("books")]
    OrderBook,

    /// <summary>
    /// OrderBook_5
    /// </summary>
    [Map("books5")]
    OrderBook_5,

    /// <summary>
    /// OrderBook_50_l2_TBT
    /// </summary>
    [Map("books50-l2-tbt")]
    OrderBook_50_l2_TBT,

    /// <summary>
    /// OrderBook_l2_TBT
    /// </summary>
    [Map("books-l2-tbt")]
    OrderBook_l2_TBT,

    /// <summary>
    /// BBO_TBT
    /// </summary>
    [Map("bbo-tbt")]
    BBO_TBT
}