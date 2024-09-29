namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Order State
/// </summary>
public enum OkxFundingConvertOrderState
{
    /// <summary>
    /// fullyFilled: success
    /// </summary>
    Success,

    /// <summary>
    /// rejected: failed
    /// </summary>
    Failed,
}