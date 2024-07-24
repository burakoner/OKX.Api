namespace OKX.Api.Trade.Enums;

/// <summary>
/// OKX Position Type
/// </summary>
public enum OkxPositionType
{
    /// <summary>
    /// Contracts of pending orders and open positions for all derivatives instruments.
    /// </summary>
    ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstruments,

    /// <summary>
    /// Contracts of pending orders for all derivatives instruments.
    /// </summary>
    ContractsOfPendingOrdersForAllDerivativesInstruments,

    /// <summary>
    /// Pending orders for all derivatives instruments.
    /// </summary>
    PendingOrdersForAllDerivativesInstruments,

    /// <summary>
    /// Contracts of pending orders and open positions for all derivatives instruments on the same side.
    /// </summary>
    ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstrumentsOnTheSameSide,

    /// <summary>
    /// Pending orders for one derivatives instrument.
    /// </summary>
    PendingOrdersForOneDerivativesInstrument,

    /// <summary>
    /// Contracts of pending orders and open positions for one derivatives instrument.
    /// </summary>
    ContractsOfPendingOrdersAndOpenPositionsForOneDerivativesInstrument,

    /// <summary>
    /// Contracts of one pending order.
    /// </summary>
    ContractsOfOnePendingOrder,
}