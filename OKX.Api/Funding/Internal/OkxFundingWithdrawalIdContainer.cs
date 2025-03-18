namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Id
/// </summary>
internal record OkxFundingWithdrawalIdContainer
{
    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long Payload { get; set; }
}