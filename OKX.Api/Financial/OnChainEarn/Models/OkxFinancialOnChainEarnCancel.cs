namespace OKX.Api.Financial.OnChainEarn.Models;

/// <summary>
/// OKX Financial OnChainEarn Cancel
/// </summary>
public class OkxFinancialOnChainEarnCancel
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