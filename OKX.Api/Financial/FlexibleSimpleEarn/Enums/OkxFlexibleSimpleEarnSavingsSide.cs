namespace OKX.Api.Financial.FlexibleSimpleEarn;

/// <summary>
/// OKX Flexible Simple Earn Savings Side
/// </summary>
public enum OkxFlexibleSimpleEarnSavingsSide
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