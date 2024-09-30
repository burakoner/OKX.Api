namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Sub-Order Status
/// </summary>
public enum OkxGridAlgoSubOrderStatus
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live,

    /// <summary>
    /// Partial
    /// </summary>
    [Map("partially_filled")]
    Partial,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("cancelling")]
    Cancelling,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled,
}