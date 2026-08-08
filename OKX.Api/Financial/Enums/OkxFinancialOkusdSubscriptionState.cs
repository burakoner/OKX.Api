namespace OKX.Api.Financial;

/// <summary>
/// OKUSD Subscription State
/// </summary>
public enum OkxFinancialOkusdSubscriptionState : byte
{
    /// <summary>
    /// Subscription succeeded
    /// </summary>
    [Map("success")]
    Success = 1,

    /// <summary>
    /// Subscription is pending
    /// </summary>
    [Map("pending")]
    Pending = 2,

    /// <summary>
    /// Subscription failed
    /// </summary>
    [Map("failed")]
    Failed = 3,
}