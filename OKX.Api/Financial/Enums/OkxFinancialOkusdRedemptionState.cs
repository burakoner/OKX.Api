namespace OKX.Api.Financial;

/// <summary>
/// OKUSD Redemption State
/// </summary>
public enum OkxFinancialOkusdRedemptionState : byte
{
    /// <summary>
    /// Redemption is processing
    /// </summary>
    [Map("processing")]
    Processing = 1,

    /// <summary>
    /// Redemption succeeded
    /// </summary>
    [Map("success")]
    Success = 2,

    /// <summary>
    /// Redemption failed
    /// </summary>
    [Map("failed")]
    Failed = 3,

    /// <summary>
    /// Redemption was cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled = 4,
}