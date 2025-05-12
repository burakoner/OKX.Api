namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Product Info
/// </summary>
public record OkxFinancialProductInfo
{
    /// <summary>
    /// Fast redemption daily limit
    /// The master account and sub-accounts share the same limit
    /// </summary>
    [JsonProperty("fastRedemptionDailyLimit")]
    public decimal? FastRedemptionDailyLimit { get; set; }

    /// <summary>
    /// Currently fast redemption max available amount
    /// </summary>
    [JsonProperty("fastRedemptionAvail")]
    public decimal? FastRedemptionAvailable { get; set; }
}