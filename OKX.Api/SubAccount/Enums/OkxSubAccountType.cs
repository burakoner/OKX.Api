namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account Type
/// </summary>
public enum OkxSubAccountType
{
    /// <summary>
    /// Standard
    /// </summary>
    [Map("1")]
    Standard,

    /// <summary>
    /// Custody
    /// </summary>
    [Map("2")]
    Custody,
}