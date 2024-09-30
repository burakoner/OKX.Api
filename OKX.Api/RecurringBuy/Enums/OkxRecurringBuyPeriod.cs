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
    Monthly,

    /// <summary>
    /// Weekly
    /// </summary>
    [Map("weekly")]
    Weekly,

    /// <summary>
    /// Daily
    /// </summary>
    [Map("daily")]
    Daily,

    /// <summary>
    /// Hourly
    /// </summary>
    [Map("hourly")]
    Hourly
}