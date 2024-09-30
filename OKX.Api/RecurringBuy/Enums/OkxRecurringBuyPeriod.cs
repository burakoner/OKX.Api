namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX Recurring Buy Period
/// </summary>
public enum OkxRecurringBuyPeriod
{
    /// <summary>
    /// Monthly
    /// </summary>
    [Map("monthly")]
    Monthly = 4,

    /// <summary>
    /// Weekly
    /// </summary>
    [Map("weekly")]
    Weekly = 3,

    /// <summary>
    /// Daily
    /// </summary>
    [Map("daily")]
    Daily = 2,

    /// <summary>
    /// Hourly
    /// </summary>
    [Map("hourly")]
    Hourly = 1
}