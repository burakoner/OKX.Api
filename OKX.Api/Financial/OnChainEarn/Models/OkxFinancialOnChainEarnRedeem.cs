namespace OKX.Api.Financial.OnChainEarn.Models;

/// <summary>
/// OKX Financial OnChainEarn Redeem
/// </summary>
public class OkxFinancialOnChainEarnRedeem
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; }

    ///// <summary>
    ///// Tag
    ///// </summary>
    //[JsonProperty("tag")]
    //public string Tag { get; set; }
}