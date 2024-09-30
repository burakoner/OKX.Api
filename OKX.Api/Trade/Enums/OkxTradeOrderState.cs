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
    Live,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled,

    /// <summary>
    /// PartiallyFilled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Order cancelled automatically due to Market Maker Protection
    /// </summary>
    [Map("mmp_canceled")]
    MmpCanceled
}