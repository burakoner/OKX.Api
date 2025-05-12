namespace OKX.Api.Funding;

/// <summary>
/// OKX Deposit State
/// </summary>
public enum OkxFundingDepositState : byte
{
    /// <summary>
    /// WaitingForConfirmation
    /// </summary>
    [Map("1")]
    WaitingForConfirmation = 1,

    /// <summary>
    /// Credited
    /// </summary>
    [Map("2")]
    Credited = 2,

    /// <summary>
    /// Successful
    /// </summary>
    [Map("3")]
    Successful = 3,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("4")]
    Pending = 4,

    /// <summary>
    /// pending due to temporary deposit suspension on this crypto currency
    /// </summary>
    [Map("8")]
    PendingDueToTemporaryDepositSuspension = 8,

    /// <summary>
    /// match the address blacklist
    /// </summary>
    [Map("11")]
    MatchTheAddressBlacklist = 11,

    /// <summary>
    /// account or deposit is frozen
    /// </summary>
    [Map("12")]
    AccountOrDepositIsFrozen = 12,

    /// <summary>
    /// sub-account deposit interception
    /// </summary>
    [Map("13")]
    SubAccountDepositInterception = 13,

    /// <summary>
    /// KYC limit
    /// </summary>
    [Map("14")]
    KycLimit = 14,
}