namespace OKX.Api.Financial;

/// <summary>
/// OKX Flexible Simple Earn Savings Side
/// </summary>
public enum OkxFinancialSimpleEarnSavingsSide : byte
{
    /// <summary>
    /// Purchase
    /// </summary>
    [Map("purchase")]
    Purchase = 1,

    /// <summary>
    /// Redeem
    /// </summary>
    [Map("redempt")]
    Redeem = 2,
}