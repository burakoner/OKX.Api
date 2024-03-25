namespace OKX.Api.Common.Enums;

/// <summary>
/// OKX Maintenance Service
/// </summary>
public enum OkxMaintenanceService
{
    /// <summary>
    /// WebSocket
    /// </summary>
    WebSocket,

    /*
    /// <summary>
    /// SpotMargin
    /// </summary>
    SpotMargin,

    /// <summary>
    /// Futures
    /// </summary>
    Futures,

    /// <summary>
    /// Perpetual
    /// </summary>
    Perpetual,

    /// <summary>
    /// Options
    /// </summary>
    Options,
    */

    /// <summary>
    /// TradingService
    /// </summary>
    TradingService,

    /// <summary>
    /// BlockTrading
    /// </summary>
    BlockTrading,

    /// <summary>
    /// TradingBot
    /// </summary>
    TradingBot,

    /// <summary>
    /// Trading Service (In Batches Of Accounts)
    /// </summary>
    TradingServiceInBatchesOfAccounts,

    /// <summary>
    /// Trading Service (In Batches Of Products)
    /// </summary>
    TradingServiceInBatchesOfProducts,

    /// <summary>
    /// SpreadTrading
    /// </summary>
    SpreadTrading,

    /// <summary>
    /// CopyTrading
    /// </summary>
    CopyTrading,

    /// <summary>
    /// Others
    /// </summary>
    Others
}