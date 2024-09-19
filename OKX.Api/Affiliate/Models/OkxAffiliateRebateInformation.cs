namespace OKX.Api.Affiliate.Models;

/// <summary>
/// OKX Affiliate Rebate Information
/// </summary>
public class OkxAffiliateRebateInformation
{
    /// <summary>
    /// Whether the user is invited by the current affiliate. true, false
    /// </summary>
    [JsonProperty("result")]
    public bool Result { get; set; }
    
    /// <summary>
    /// Whether there is affiliate rebate.
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }
}
