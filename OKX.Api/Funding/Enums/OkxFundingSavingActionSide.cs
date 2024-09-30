namespace OKX.Api.Funding;

/// <summary>
/// OKX Saving Action Side
/// </summary>
public enum OkxFundingSavingActionSide
{
    /// <summary>
    /// Purchase
    /// </summary>
    [Map("purchase")]
    Purchase = 1,

    /// <summary>
    /// Redempt
    /// </summary>
    [Map("redempt")]
    Redempt = 2,
}