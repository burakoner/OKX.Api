namespace OKX.Api.Public;

/// <summary>
/// Security fund detail type
/// </summary>
public enum OkxPublicInsuranceFundDetailType : byte
{
    /// <summary>
    /// Undocumented real-time security fund snapshot row still observed in production responses after its documented removal
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
    /// Platform revenue. Deprecated by OKX and currently returns empty values.
    /// </summary>
    [Obsolete("OKX has deprecated platform_revenue; it currently returns empty values and is scheduled for removal.")]
    [Map("platform_revenue")]
    PlatformRevenue = 4,

    /// <summary>
    /// ADL historical row. Deprecated by OKX and currently returns empty values.
    /// </summary>
    [Obsolete("OKX has deprecated adl; it currently returns empty values and is scheduled for removal.")]
    [Map("adl")]
    Adl = 5,
}
