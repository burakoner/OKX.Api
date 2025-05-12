namespace OKX.Api.Block;

/// <summary>
/// OKX Block State
/// </summary>
public enum OkxBlockState : byte
{
    /// <summary>
    /// Active
    /// </summary>
    [Map("active")]
    Active=1,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled,

    /// <summary>
    /// Pending Fill
    /// </summary>
    [Map("pending_fill")]
    PendingFill,

    /// <summary>
    /// Traded Away
    /// </summary>
    [Map("traded_away")]
    TradedAway,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed
}
