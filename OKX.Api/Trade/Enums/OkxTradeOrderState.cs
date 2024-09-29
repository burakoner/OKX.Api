namespace OKX.Api.Trade;

/// <summary>
/// OKX Order State
/// </summary>
public enum OkxTradeOrderState
{
    /// <summary>
    /// Live
    /// </summary>
    Live,

    /// <summary>
    /// Canceled
    /// </summary>
    Canceled,

    /// <summary>
    /// PartiallyFilled
    /// </summary>
    PartiallyFilled,

    /// <summary>
    /// Filled
    /// </summary>
    Filled,

    /// <summary>
    /// Order cancelled automatically due to Market Maker Protection
    /// </summary>
    MmpCanceled
}