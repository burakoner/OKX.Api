namespace OKX.Api.Public;

/// <summary>
/// OKX Convert Unit
/// </summary>
public enum OkxPublicConvertUnit : byte
{
    /// <summary>
    /// Coin
    /// </summary>
    [Map("coin")]
    Coin = 1,

    /// <summary>
    /// Usds
    /// </summary>
    [Map("usds")]
    USDs = 2,
}