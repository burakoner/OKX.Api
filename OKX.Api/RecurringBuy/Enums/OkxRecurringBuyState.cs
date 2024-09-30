namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX Recurring Buy State
/// </summary>
public enum OkxRecurringBuyState
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("canceled")]
    Live = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("live")]
    Filled = 2,

    /// <summary>
    /// PartiallyFilled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled = 3,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("filled")]
    Cancelling = 4,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("cancelling")]
    Canceled = 5
}