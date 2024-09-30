namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Sub Position Type
/// </summary>
public enum OkxCopyTradingPositionType
{
    /// <summary>
    /// Lead
    /// </summary>
    [Map("lead")]
    Lead,

    /// <summary>
    /// Copy
    /// </summary>
    [Map("copy")]
    Copy
}