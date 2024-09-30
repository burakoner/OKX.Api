namespace OKX.Api.Public;

/// <summary>
/// OKX Insurance Type
/// </summary>
public enum OkxPublicInsuranceType
{
    /// <summary>
    /// All
    /// </summary>
    [Map("all")]
    All = 1,

    /// <summary>
    /// LiquidationBalanceDeposit
    /// </summary>
    [Map("liquidation_balance_deposit")]
    LiquidationBalanceDeposit = 2,

    /// <summary>
    /// BankruptcyLoss
    /// </summary>
    [Map("bankruptcy_loss")]
    BankruptcyLoss = 3,

    /// <summary>
    /// PlatformRevenue
    /// </summary>
    [Map("platform_revenue")]
    PlatformRevenue = 4,
}