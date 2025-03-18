namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Daily Limit Container
/// </summary>
internal record OkxFinancialDailyLimitContainer
{
    /// <summary>
    /// Fast redemption daily limit
    /// The master account and sub-accounts share the same limit
    /// </summary>
    [JsonProperty("fastRedemptionDailyLimit")]
    public decimal? Payload { get; set; }
}