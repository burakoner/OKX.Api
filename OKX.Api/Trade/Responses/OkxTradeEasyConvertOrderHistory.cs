namespace OKX.Api.Trade;

/// <summary>
/// OKX Easy Convert Order History
/// </summary>
public class OkxTradeEasyConvertOrderHistory : OkxTradeEasyConvertOrder
{
    /// <summary>
    /// Account
    /// </summary>
    [JsonProperty("acct")]
    public OkxAccount Account { get; set; }
}