namespace OKX.Api.Trade;

/// <summary>
/// OKX Options Price Type
/// </summary>
public enum OkxTradeOptionsPriceType
{
    /// <summary>
    /// Place an order based on price, in the unit of coin (the unit for the request parameter px is BTC or ETH)
    /// </summary>
    [Map("px")]
    Price = 1,

    /// <summary>
    /// Place an order based on pxUsd, in the unit of USD (the unit for the request parameter px is USD)
    /// </summary>
    [Map("pxUsd")]
    PriceUsd = 2,

    /// <summary>
    /// Place an order based on pxVol
    /// </summary>
    [Map("pxVol")]
    PriceVolatility = 3,
}