namespace OKX.Api.Funding;

/// <summary>
/// OKX Transfer State
/// </summary>
public enum OkxFundingTransferState
{
    /// <summary>
    /// Success
    /// </summary>
    [Map("success")]
    Success,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("pending")]
    Pending,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,
}