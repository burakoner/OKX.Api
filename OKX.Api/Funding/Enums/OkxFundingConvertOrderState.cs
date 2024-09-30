namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Order State
/// </summary>
public enum OkxFundingConvertOrderState
{
    /// <summary>
    /// fullyFilled: success
    /// </summary>
    [Map("fullyFilled")]
    Success,

    /// <summary>
    /// rejected: failed
    /// </summary>
    [Map("rejected")]
    Failed,
}