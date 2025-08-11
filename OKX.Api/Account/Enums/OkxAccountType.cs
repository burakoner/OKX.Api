namespace OKX.Api.Account;

/// <summary>
/// OKX Account Type
/// </summary>
public enum OkxAccountType : byte
{
    /// <summary>
    /// Main Account
    /// </summary>
    [Map("0")]
    MainAccount = 0,

    /// <summary>
    /// Standard sub-account
    /// </summary>
    [Map("1")]
    StandardSubAccount = 1,

    /// <summary>
    /// Managed trading sub-account
    /// </summary>
    [Map("2")]
    ManagedTradingSubAccount = 2,

    /// <summary>
    /// Custody trading sub-account - Copper
    /// </summary>
    [Map("5")]
    CopperCustodyTradingSubAccount = 5,

    /// <summary>
    /// Managed trading sub-account - Copper
    /// </summary>
    [Map("9")]
    CopperManagedTradingSubAccount = 9,

    /// <summary>
    /// Custody trading sub-account - Komainu
    /// </summary>
    [Map("12")]
    KomainuCustodyTradingSubAccount = 12,
}