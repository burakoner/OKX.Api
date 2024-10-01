namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Sub Position Type
/// </summary>
public enum OkxCopyTradingPositionType
{
    /// <summary>
    /// Lead
    /// </summary>
    [Map("lead")]
    Lead = 1,

    /// <summary>
    /// Copy
    /// </summary>
    [Map("copy")]
    Copy = 2
}