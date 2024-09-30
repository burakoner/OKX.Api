namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial OnChainEarning Type
/// </summary>
public enum OkxFinancialOnChainEarnEarningType
{
    /// <summary>
    /// Estimated earning
    /// </summary>
    [Map("0")]
    EstimatedEarning = 0,

    /// <summary>
    /// Cumulative earning
    /// </summary>
    [Map("1")]
    CumulativeEarning = 1,
}