namespace OKX.Api.Trade;

/// <summary>
/// OKX Quantity Type
/// </summary>
public enum OkxTradeQuantityType : byte
{
    /// <summary>
    /// BaseCurrency
    /// </summary>
    [Map("base_ccy")]
    BaseCurrency = 1,

    /// <summary>
    /// QuoteCurrency
    /// </summary>
    [Map("quote_ccy")]
    QuoteCurrency = 2,
}