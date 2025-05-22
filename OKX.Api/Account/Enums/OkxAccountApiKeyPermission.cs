namespace OKX.Api.Account;

/// <summary>
/// OKX Api Key Permission
/// </summary>
public enum OkxAccountApiKeyPermission : byte
{
    /// <summary>
    /// ReadOnly
    /// </summary>
    [Map("read_only")]
    ReadOnly = 1,

    /// <summary>
    /// Trade
    /// </summary>
    [Map("trade")]
    Trade = 2,

    /// <summary>
    /// Withdraw
    /// </summary>
    [Map("withdraw")]
    Withdraw = 3
}