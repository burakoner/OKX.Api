namespace OKX.Api.Account;

/// <summary>
/// OKX Account Forced Repayment Type
/// </summary>
public enum OkxAccountForcedRepaymentType : byte
{
    /// <summary>
    /// no FRP
    /// </summary>
    [Map("0")]
    NoFRP = 0,

    /// <summary>
    /// user based FRP
    /// </summary>
    [Map("1")]
    UserBasedFRP = 1,

    /// <summary>
    /// platform based FRP
    /// </summary>
    [Map("2")]
    PlatformBasedFRP = 2,
}