namespace OKX.Api.Account;

/// <summary>
/// OKX Api Key Permission
/// </summary>
public enum OkxAccountApiKeyPermission
{
    /// <summary>
    /// ReadOnly
    /// </summary>
    [Map("read_only")]
    ReadOnly,

    /// <summary>
    /// Trade
    /// </summary>
    [Map("trade")]
    Trade,

    /// <summary>
    /// Withdraw
    /// </summary>
    [Map("withdraw")]
    Withdraw
}