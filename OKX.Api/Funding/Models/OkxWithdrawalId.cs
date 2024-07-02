namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Withdrawal Id
/// </summary>
public class OkxWithdrawalId
{
    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }
}