namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Withdrawal Id
/// </summary>
public class OkxFundingWithdrawalId
{
    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }
}