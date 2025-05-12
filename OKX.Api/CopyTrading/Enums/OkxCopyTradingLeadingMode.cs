namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Leading Mode
/// </summary>
public enum OkxCopyTradingLeadingMode : byte
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