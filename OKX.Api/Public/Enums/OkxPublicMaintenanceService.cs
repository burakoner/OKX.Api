namespace OKX.Api.Public;

/// <summary>
/// OKX Maintenance Service
/// </summary>
public enum OkxPublicMaintenanceService : byte
{
    /// <summary>
    /// WebSocket
    /// </summary>
    [Map("0")]
    WebSocket = 0,

    /*
    /// <summary>
    /// SpotMargin
    /// </summary>
    [Map("1")]
    SpotMargin = 1,

    /// <summary>
    /// Futures
    /// </summary>
    [Map("2")]
    Futures = 2,

    /// <summary>
    /// Perpetual
    /// </summary>
    [Map("3")]
    Perpetual = 3,

    /// <summary>
    /// Options
    /// </summary>
    [Map("4")]
    Options = 4,
    */

    /// <summary>
    /// TradingService
    /// </summary>
    [Map("5")]
    TradingService = 5,

    /// <summary>
    /// BlockTrading
    /// </summary>
    [Map("6")]
    BlockTrading = 6,

    /// <summary>
    /// TradingBot
    /// </summary>
    [Map("7")]
    TradingBot = 7,

    /// <summary>
    /// Trading Service (In Batches Of Accounts)
    /// </summary>
    [Map("8")]
    TradingServiceInBatchesOfAccounts = 8,

    /// <summary>
    /// Trading Service (In Batches Of Products)
    /// </summary>
    [Map("9")]
    TradingServiceInBatchesOfProducts = 9,

    /// <summary>
    /// SpreadTrading
    /// </summary>
    [Map("10")]
    SpreadTrading = 10,

    /// <summary>
    /// CopyTrading
    /// </summary>
    [Map("11")]
    CopyTrading = 11,

    /// <summary>
    /// Others
    /// </summary>
    [Map("99")]
    Others = 99
}