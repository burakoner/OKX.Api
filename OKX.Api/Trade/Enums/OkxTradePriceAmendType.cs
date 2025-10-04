namespace OKX.Api.Trade;

/// <summary>
/// OKX Trade Price Amend Type
/// </summary>
public enum OkxTradePriceAmendType : byte
{
    /// <summary>
    /// Do not allow the system to amend to order price if px exceeds the price limit
    /// </summary>
    [Map("0")]
    DoNotAllowSystemToAmendPrice = 0,

    /// <summary>
    /// Allow the system to amend the price to the best available value within the price limit if px exceeds the price limit
    /// </summary>
    [Map("1")]
    AllowSystemToAmendPrice = 1,
}