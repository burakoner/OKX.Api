namespace OKX.Api.Trade;

/// <summary>
/// OKX Order State
/// </summary>
public enum OkxTradeOrderState
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled = 2,

    /// <summary>
    /// PartiallyFilled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled = 3,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 4,

    /// <summary>
    /// Order cancelled automatically due to Market Maker Protection
    /// </summary>
    [Map("mmp_canceled")]
    MmpCanceled = 5
}