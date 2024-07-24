using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX Easy Convert Order History
/// </summary>
public class OkxEasyConvertOrderHistory : OkxEasyConvertOrder
{
    /// <summary>
    /// Account
    /// </summary>
    [JsonProperty("acct"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount Account { get; set; }
}