namespace OKX.Api.Trade.Enums;

/// <summary>
/// OKX Options Price Type
/// </summary>
public enum OkxTradeOptionsPriceType
{
    /// <summary>
    /// Place an order based on price, in the unit of coin (the unit for the request parameter px is BTC or ETH)
    /// </summary>
    Price,

    /// <summary>
    /// Place an order based on pxUsd, in the unit of USD (the unit for the request parameter px is USD)
    /// </summary>
    PriceUsd,

    /// <summary>
    /// Place an order based on pxVol
    /// </summary>
    PriceVolatility,
}