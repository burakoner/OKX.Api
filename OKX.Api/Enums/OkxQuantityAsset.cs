namespace OKX.Api.Enums;

public enum OkxQuantityAsset
{
    /// <summary>
    /// Quantity in base asset
    /// </summary>
    [Map("base_ccy")]
    BaseAsset,
    /// <summary>
    /// Quantity in quote asset
    /// </summary>
    [Map("quote_ccy")]
    QuoteAsset
}