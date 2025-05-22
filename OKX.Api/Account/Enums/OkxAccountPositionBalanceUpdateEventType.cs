namespace OKX.Api.Account;

/// <summary>
/// OKX Account
/// </summary>
public enum OkxAccountPositionBalanceUpdateEventType : byte
{
    /// <summary>
    /// Snapshot
    /// </summary>
    [Map("snapshot")]
    Snapshot = 1,

    /// <summary>
    /// Delivered
    /// </summary>
    [Map("delivered")]
    Delivered = 2,

    /// <summary>
    /// Exercised
    /// </summary>
    [Map("exercised")]
    Exercised = 3,

    /// <summary>
    /// Transferred
    /// </summary>
    [Map("transferred")]
    Transferred = 4,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 5,

    /// <summary>
    /// Liquidation
    /// </summary>
    [Map("liquidation")]
    Liquidation = 6,

    /// <summary>
    /// Claw back
    /// </summary>
    [Map("claw_back")]
    ClawBack = 7,

    /// <summary>
    /// Adl
    /// </summary>
    [Map("adl")]
    Adl = 8,

    /// <summary>
    /// Funding fee
    /// </summary>
    [Map("funding_fee")]
    FundingFee = 9,

    /// <summary>
    /// Adjust margin
    /// </summary>
    [Map("adjust_margin")]
    AdjustMargin = 10,

    /// <summary>
    /// Set leverage
    /// </summary>
    [Map("set_leverage")]
    SetLeverage = 11,

    /// <summary>
    /// Interest deduction
    /// </summary>
    [Map("interest_deduction")]
    InterestDeduction = 12,

    /// <summary>
    /// Settlement
    /// </summary>
    [Map("settlement")]
    Settlement = 13,
}