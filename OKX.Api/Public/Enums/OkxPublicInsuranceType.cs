namespace OKX.Api.Public;

/// <summary>
/// OKX Insurance Type
/// </summary>
public enum OkxPublicInsuranceType : byte
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
    /// Platform revenue. Deprecated by OKX and currently returns empty values.
    /// </summary>
    [Obsolete("OKX has deprecated platform_revenue; it currently returns empty values and is scheduled for removal.")]
    [Map("platform_revenue")]
    PlatformRevenue = 4,

    /// <summary>
    /// ADL history. Deprecated by OKX and currently returns empty values.
    /// </summary>
    [Obsolete("OKX has deprecated adl; it currently returns empty values and is scheduled for removal.")]
    [Map("adl")]
    Adl = 5,
}
