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

    /// <summary>
    /// Commodities
    /// </summary>
    [Map("4")]
    Commodities = 4,

    /// <summary>
    /// Forex
    /// </summary>
    [Map("5")]
    Forex = 5,

    /// <summary>
    /// Bonds
    /// </summary>
    [Map("6")]
    Bonds = 6,
}
