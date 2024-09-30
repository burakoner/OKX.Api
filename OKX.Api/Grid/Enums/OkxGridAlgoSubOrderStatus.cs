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
    Live = 1,

    /// <summary>
    /// Partial
    /// </summary>
    [Map("partially_filled")]
    Partial = 2,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 3,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("cancelling")]
    Cancelling = 4,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled = 5,
}