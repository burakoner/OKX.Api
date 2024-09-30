namespace OKX.Api.Funding;

/// <summary>
/// OKX Transfer State
/// </summary>
public enum OkxFundingTransferState
{
    /// <summary>
    /// Pending
    /// </summary>
    [Map("pending")]
    Pending = 1,

    /// <summary>
    /// Success
    /// </summary>
    [Map("success")]
    Success = 2,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed = 3,
}