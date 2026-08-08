namespace OKX.Api.CopyTrading;

/// <summary>
/// Copy trading performance period
/// </summary>
public enum OkxCopyTradingPerformancePeriod : byte
{
    /// <summary>
    /// Last 7 days
    /// </summary>
    [Map("1")]
    Last7Days = 1,

    /// <summary>
    /// Last 30 days
    /// </summary>
    [Map("2")]
    Last30Days = 2,

    /// <summary>
    /// Last 90 days
    /// </summary>
    [Map("3")]
    Last90Days = 3,

    /// <summary>
    /// Last 365 days
    /// </summary>
    [Map("4")]
    Last365Days = 4,
}
