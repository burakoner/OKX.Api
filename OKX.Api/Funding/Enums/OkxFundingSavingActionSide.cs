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
    Purchase,

    /// <summary>
    /// Redempt
    /// </summary>
    [Map("redempt")]
    Redempt,
}