namespace OKX.Api.Trade;

/// <summary>
/// OKX Easy Convert Order History
/// </summary>
public record OkxTradeEasyConvertOrderHistory : OkxTradeEasyConvertOrder
{
    /// <summary>
    /// Account
    /// </summary>
    [JsonProperty("acct")]
    public OkxAccount Account { get; set; }
}