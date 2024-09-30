namespace OKX.Api.Trade;

/// <summary>
/// OKX Position Type
/// </summary>
public enum OkxTradePositionType
{
    /// <summary>
    /// Contracts of pending orders and open positions for all derivatives instruments.
    /// </summary>
    [Map("1")]
    ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstruments,

    /// <summary>
    /// Contracts of pending orders for all derivatives instruments.
    /// </summary>
    [Map("2")]
    ContractsOfPendingOrdersForAllDerivativesInstruments,

    /// <summary>
    /// Pending orders for all derivatives instruments.
    /// </summary>
    [Map("3")]
    PendingOrdersForAllDerivativesInstruments,

    /// <summary>
    /// Contracts of pending orders and open positions for all derivatives instruments on the same side.
    /// </summary>
    [Map("4")]
    ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstrumentsOnTheSameSide,

    /// <summary>
    /// Pending orders for one derivatives instrument.
    /// </summary>
    [Map("5")]
    PendingOrdersForOneDerivativesInstrument,

    /// <summary>
    /// Contracts of pending orders and open positions for one derivatives instrument.
    /// </summary>
    [Map("6")]
    ContractsOfPendingOrdersAndOpenPositionsForOneDerivativesInstrument,

    /// <summary>
    /// Contracts of one pending order.
    /// </summary>
    [Map("7")]
    ContractsOfOnePendingOrder,
}