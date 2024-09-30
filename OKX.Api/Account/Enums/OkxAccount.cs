namespace OKX.Api.Account;

/// <summary>
/// OKX Account
/// </summary>
public enum OkxAccount
{
    /// <summary>
    /// Funding Account
    /// </summary>
    [Map("6")]
    Funding = 6,

    /// <summary>
    /// Trading Account
    /// </summary>
    [Map("18")]
    Trading = 18,
}