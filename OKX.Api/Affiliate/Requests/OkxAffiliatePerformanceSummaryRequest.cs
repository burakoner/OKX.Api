namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate performance summary query.
/// </summary>
public record OkxAffiliatePerformanceSummaryRequest
{
    /// <summary>
    /// Statistics window. The server default is lifetime statistics.
    /// </summary>
    public OkxAffiliatePeriodType? PeriodType { get; set; }

    /// <summary>
    /// Inclusive custom-window start, as a Unix timestamp in milliseconds.
    /// </summary>
    public long? Begin { get; set; }

    /// <summary>
    /// Inclusive custom-window end, as a Unix timestamp in milliseconds.
    /// </summary>
    public long? End { get; set; }
}
