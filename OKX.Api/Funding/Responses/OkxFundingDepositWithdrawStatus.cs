namespace OKX.Api.Funding;

/// <summary>
/// OKX deposit/withdraw status
/// </summary>
public record OkxFundingDepositWithdrawStatus
{
    /// <summary>
    /// Withdrawal ID. Blank when retrieving deposit status.
    /// </summary>
    [JsonProperty("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;

    /// <summary>
    /// Hash record on-chain. Can be blank before a withdrawal txId is generated.
    /// </summary>
    [JsonProperty("txId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// The detailed stage and status of the deposit/withdrawal.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Estimated complete time in UTC+8.
    /// </summary>
    [JsonProperty("estCompleteTime")]
    public string EstimatedCompleteTime { get; set; } = string.Empty;
}
