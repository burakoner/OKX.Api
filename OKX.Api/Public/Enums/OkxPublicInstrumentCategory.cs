namespace OKX.Api.Public;

/// <summary>
/// OKX instrument base-asset category
/// </summary>
public enum OkxPublicInstrumentCategory : byte
{
    /// <summary>
    /// Crypto
    /// </summary>
    [Map("1")]
    Crypto = 1,

    /// <summary>
    /// Stocks
    /// </summary>
    [Map("3")]
    Stocks = 3,
}
