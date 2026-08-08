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
    /// The RFQ was executed against another maker's quote. This state only applies to makers.
    /// </summary>
    [Map("traded_away")]
    TradedAway,

    /// <summary>
    /// The RFQ was successfully executed against the maker's quote.
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
