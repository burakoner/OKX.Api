namespace OKX.Api.Status;

/// <summary>
/// OKX Maintenance Service
/// </summary>
public enum OkxStatusMaintenanceService
{
    /// <summary>
    /// WebSocket
    /// </summary>
    [Map("0")]
    WebSocket,

    /*
    /// <summary>
    /// SpotMargin
    /// </summary>
    [Map("1")]
    SpotMargin,

    /// <summary>
    /// Futures
    /// </summary>
    [Map("2")]
    Futures,

    /// <summary>
    /// Perpetual
    /// </summary>
    [Map("3")]
    Perpetual,

    /// <summary>
    /// Options
    /// </summary>
    [Map("4")]
    Options,
    */

    /// <summary>
    /// TradingService
    /// </summary>
    [Map("5")]
    TradingService,

    /// <summary>
    /// BlockTrading
    /// </summary>
    [Map("6")]
    BlockTrading,

    /// <summary>
    /// TradingBot
    /// </summary>
    [Map("7")]
    TradingBot,

    /// <summary>
    /// Trading Service (In Batches Of Accounts)
    /// </summary>
    [Map("8")]
    TradingServiceInBatchesOfAccounts,

    /// <summary>
    /// Trading Service (In Batches Of Products)
    /// </summary>
    [Map("9")]
    TradingServiceInBatchesOfProducts,

    /// <summary>
    /// SpreadTrading
    /// </summary>
    [Map("10")]
    SpreadTrading,

    /// <summary>
    /// CopyTrading
    /// </summary>
    [Map("11")]
    CopyTrading,

    /// <summary>
    /// Others
    /// </summary>
    [Map("99")]
    Others
}