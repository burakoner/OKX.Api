using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX Easy Convert Order History
/// </summary>
public class OkxTradeEasyConvertOrderHistory : OkxTradeEasyConvertOrder
{
    /// <summary>
    /// Account
    /// </summary>
    [JsonProperty("acct"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount Account { get; set; }
}