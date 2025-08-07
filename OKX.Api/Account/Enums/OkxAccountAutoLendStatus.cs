namespace OKX.Api.Account;

/// <summary>
/// OKX Account Auto Lend Status
/// </summary>
public enum OkxAccountAutoLendStatus : byte
{
    /// <summary>
    /// auto lend is not supported by this currency
    /// </summary>
    [Map("unsupported")]
    Unsupported = 0,

    /// <summary>
    /// auto lend is supported but turned off
    /// </summary>
    [Map("off")]
    Off = 1,

    /// <summary>
    /// auto lend is turned on but pending matching
    /// </summary>
    [Map("pending")]
    Pending = 2,

    /// <summary>
    /// auto lend is turned on and matched
    /// </summary>
    [Map("active")]
    Active = 3,
}