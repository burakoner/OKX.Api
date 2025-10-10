namespace OKX.Api.Trade;

/// <summary>
/// Okx Trade Order Precheck Type
/// </summary>
public enum OkxTradeOrderPrecheckType : byte
{
    /// <summary>
    /// it is both base currency before and after placing order
    /// </summary>
    [Map("cross")]
    BothBaseCurrencyBeforeAndAfterPlacingOrder = 1,

    /// <summary>
    /// before plaing order, it is base currency. after placing order, it is quota currency.
    /// </summary>
    [Map("2")]
    BeforePlacingOrderBaseCurrency = 2,

    /// <summary>
    /// before plaing order, it is quota currency. after placing order, it is base currency
    /// </summary>
    [Map("3")]
    BeforePlacingOrderQuoteCurrency = 3,

    /// <summary>
    /// it is both quota currency before and after placing order
    /// </summary>
    [Map("4")]
    BothQuoteCurrencyBeforeAndAfterPlacingOrder = 4,
}