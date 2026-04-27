namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX Recurring Buy Sub Order State
/// </summary>
public enum OkxRecurringBuySubOrderState : byte
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
    /// Partially Filled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled = 3,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 4,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("cancelling")]
    Cancelling = 5
}
