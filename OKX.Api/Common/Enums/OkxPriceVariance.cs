namespace OKX.Api.Common;

/// <summary>
/// OKX Price Variance
/// </summary>
public enum OkxPriceVariance : byte
{
    /// <summary>
    /// Spread
    /// </summary>
    [Map("pxSpread")]
    Spread = 1,

    /// <summary>
    /// Variance
    /// </summary>
    [Map("pxVar")]
    Variance = 2,
}