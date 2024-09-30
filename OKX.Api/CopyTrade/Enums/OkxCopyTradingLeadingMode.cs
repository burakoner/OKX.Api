namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Leading Mode
/// </summary>
public enum OkxCopyTradingLeadingMode
{
    /// <summary>
    /// Public
    /// </summary>
    [Map("public")]
    Public = 1,

    /// <summary>
    /// Private
    /// </summary>
    [Map("private")]
    Private = 2
}