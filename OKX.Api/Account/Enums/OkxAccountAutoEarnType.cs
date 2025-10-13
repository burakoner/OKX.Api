namespace OKX.Api.Account;

/// <summary>
/// OKX Account Auto Earn Type
/// </summary>
public enum OkxAccountAutoEarnType : byte
{
    /// <summary>
    /// auto lend, auto staking
    /// </summary>
    [Map("0")]
    AutoEarnAutoStaking = 0,

    /// <summary>
    /// USDG earn
    /// </summary>
    [Map("1")]
    UsdgEarn = 1,
}