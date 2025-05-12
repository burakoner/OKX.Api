namespace OKX.Api.Public;

/// <summary>
/// OKX Convert Operation
/// </summary>
public enum OkxPublicConvertOperation : byte
{
    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Close
    /// </summary>
    [Map("close")]
    Close = 2,
}