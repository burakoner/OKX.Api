namespace OKX.Api.RecurringBuy.Enums;

/// <summary>
/// OKX Recurring Buy State
/// </summary>
public enum OkxRecurringBuyState
{
    /// <summary>
    /// Live
    /// </summary>
    Live,

    /// <summary>
    /// Filled
    /// </summary>
    Filled,

    /// <summary>
    /// PartiallyFilled
    /// </summary>
    PartiallyFilled,

    /// <summary>
    /// Cancelling
    /// </summary>
    Cancelling,

    /// <summary>
    /// Canceled
    /// </summary>
    Canceled
}