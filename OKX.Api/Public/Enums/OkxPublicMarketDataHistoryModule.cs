namespace OKX.Api.Public;

/// <summary>
/// OKX Public Market Data History Module
/// </summary>
public enum OkxPublicMarketDataHistoryModule : byte
{
    /// <summary>
    /// Trade history
    /// </summary>
    [Map("1")]
    TradeHistory = 1,

    /// <summary>
    /// 1-minute candlestick
    /// </summary>
    [Map("2")]
    OneMinuteCandlestick = 2,

    /// <summary>
    /// Funding rate
    /// </summary>
    [Map("3")]
    FundingRate = 3,

    /// <summary>
    /// 400-level orderbook
    /// </summary>
    [Map("4")]
    FourHundredLevelOrderBook = 4,

    /// <summary>
    /// 5000-level orderbook
    /// </summary>
    [Map("5")]
    FiveThousandLevelOrderBook = 5,

    /// <summary>
    /// 50-level orderbook
    /// </summary>
    [Map("6")]
    FiftyLevelOrderBook = 6,

    /// <summary>
    /// Borrowing rate
    /// </summary>
    [Map("11")]
    BorrowingRate = 11,
}
