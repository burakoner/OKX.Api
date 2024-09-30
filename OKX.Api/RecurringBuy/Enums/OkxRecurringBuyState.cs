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
    Live,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("live")]
    Filled,

    /// <summary>
    /// PartiallyFilled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("filled")]
    Cancelling,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("cancelling")]
    Canceled
}