namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account Type
/// </summary>
public enum OkxSubAccountType
{
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