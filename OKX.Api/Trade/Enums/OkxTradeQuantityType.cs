namespace OKX.Api.Trade;

/// <summary>
/// OKX Quantity Type
/// </summary>
public enum OkxTradeQuantityType
{
    /// <summary>
    /// BaseCurrency
    /// </summary>
    [Map("base_ccy")]
    BaseCurrency,

    /// <summary>
    /// QuoteCurrency
    /// </summary>
    [Map("quote_ccy")]
    QuoteCurrency,
}