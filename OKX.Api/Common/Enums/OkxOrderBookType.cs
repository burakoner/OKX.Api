namespace OKX.Api.Common;

/// <summary>
/// OKX Order Book Type
/// </summary>
public enum OkxOrderBookType : byte
{
    /// <summary>
    /// OrderBook
    /// </summary>
    [Map("books")]
    OrderBook = 1,

    /// <summary>
    /// OrderBook_5
    /// </summary>
    [Map("books5")]
    OrderBook_5 = 2,

    /// <summary>
    /// OrderBook_50_l2_TBT
    /// </summary>
    [Map("books50-l2-tbt")]
    OrderBook_50_l2_TBT = 3,

    /// <summary>
    /// OrderBook_l2_TBT
    /// </summary>
    [Map("books-l2-tbt")]
    OrderBook_l2_TBT = 4,

    /// <summary>
    /// BBO_TBT
    /// </summary>
    [Map("bbo-tbt")]
    BBO_TBT = 5,

    /// <summary>
    /// Deprecated Enhanced Liquidity Program order book. Use <see cref="OrderBook_RPI"/>.
    /// </summary>
    [Obsolete("Use OrderBook_RPI. OKX accepts books-elp only through October 31, 2026.")]
    [Map("books-elp")]
    OrderBook_ELP = 6,

    /// <summary>
    /// Consolidated organic and Retail Price Improvement order book.
    /// </summary>
    [Map("books-rpi")]
    OrderBook_RPI = 7
}
