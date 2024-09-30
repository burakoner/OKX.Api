namespace OKX.Api.Account;

/// <summary>
/// OKX Account Role Type
/// </summary>
public enum OkxAccountRoleType
{
    /// <summary>
    /// General User
    /// </summary>
    [Map("0")]
    GeneralUser,

    /// <summary>
    /// Leading Trader
    /// </summary>
    [Map("1")]
    LeadingTrader,

    /// <summary>
    /// Copy Trader
    /// </summary>
    [Map("2")]
    CopyTrader
}