namespace OKX.Api.Broker;

/// <summary>
/// FD broker rebate eligibility status
/// </summary>
public enum OkxFDBrokerRebateStatus : byte
{
    /// <summary>
    /// The broker can receive a rebate.
    /// </summary>
    [Map("0")]
    Eligible = 0,

    /// <summary>
    /// Broker identification has expired.
    /// </summary>
    [Map("1")]
    BrokerIdentificationExpired = 1,

    /// <summary>
    /// The user's VIP5/6 monthly commission limit has been reached.
    /// </summary>
    [Map("2")]
    MonthlyCommissionLimitReached = 2,

    /// <summary>
    /// The user's trading fee level is VIP7 or higher.
    /// </summary>
    [Map("3")]
    VipLevelNotEligible = 3,

    /// <summary>
    /// The MSA is not entitled to a broker rebate.
    /// </summary>
    [Map("4")]
    MsaNotEligible = 4,
}
