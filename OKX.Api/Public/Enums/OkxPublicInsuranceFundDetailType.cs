namespace OKX.Api.Public;

/// <summary>
/// Security fund detail type
/// </summary>
public enum OkxPublicInsuranceFundDetailType : byte
{
    /// <summary>
    /// Real-time security fund snapshot row
    /// </summary>
    [Map("regular_update")]
    RegularUpdate = 1,

    /// <summary>
    /// Liquidation balance deposit
    /// </summary>
    [Map("liquidation_balance_deposit")]
    LiquidationBalanceDeposit = 2,

    /// <summary>
    /// Bankruptcy loss
    /// </summary>
    [Map("bankruptcy_loss")]
    BankruptcyLoss = 3,

    /// <summary>
    /// Platform revenue
    /// </summary>
    [Map("platform_revenue")]
    PlatformRevenue = 4,

    /// <summary>
    /// ADL historical row
    /// </summary>
    [Map("adl")]
    Adl = 5,
}
