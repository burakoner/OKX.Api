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
    All,

    /// <summary>
    /// LiquidationBalanceDeposit
    /// </summary>
    [Map("liquidation_balance_deposit")]
    LiquidationBalanceDeposit,

    /// <summary>
    /// BankruptcyLoss
    /// </summary>
    [Map("bankruptcy_loss")]
    BankruptcyLoss,

    /// <summary>
    /// PlatformRevenue
    /// </summary>
    [Map("platform_revenue")]
    PlatformRevenue,
}