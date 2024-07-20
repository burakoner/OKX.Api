using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trading.Converters;
using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Models;

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