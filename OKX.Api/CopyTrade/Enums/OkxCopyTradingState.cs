namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading State
/// </summary>
public enum OkxCopyTradingState
{
    /// <summary>
    /// NonCopy
    /// </summary>
    [Map("0")]
    NonCopy,

    /// <summary>
    /// Copy
    /// </summary>
    [Map("1")]
    Copy
}