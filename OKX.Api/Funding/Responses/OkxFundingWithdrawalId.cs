namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Id
/// </summary>
public record OkxFundingWithdrawalId
{
    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }
}