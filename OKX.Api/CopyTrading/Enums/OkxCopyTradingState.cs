namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading State
/// </summary>
public enum OkxCopyTradingState : byte
{
    /// <summary>
    /// NonCopy
    /// </summary>
    [Map("0")]
    NonCopy = 0,

    /// <summary>
    /// Copy
    /// </summary>
    [Map("1")]
    Copy = 1
}