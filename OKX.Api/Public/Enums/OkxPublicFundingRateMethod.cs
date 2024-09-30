namespace OKX.Api.Public;

/// <summary>
/// OKX Funding Rate Method
/// </summary>
public enum OkxPublicFundingRateMethod
{
    /// <summary>
    /// CurrentPeriod
    /// </summary>
    [Map("current_period")]
    CurrentPeriod = 1,

    /// <summary>
    /// NextPeriod
    /// </summary>
    [Map("next_period")]
    NextPeriod = 2,
}