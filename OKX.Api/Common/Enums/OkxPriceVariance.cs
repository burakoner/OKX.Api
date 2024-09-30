namespace OKX.Api.Common;

/// <summary>
/// OKX Price Variance
/// </summary>
public enum OkxPriceVariance
{
    /// <summary>
    /// Spread
    /// </summary>
    [Map("pxSpread")]
    Spread,

    /// <summary>
    /// Variance
    /// </summary>
    [Map("pxVar")]
    Variance,
}