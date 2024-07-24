namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Withdrawal Status
/// </summary>
public class OkxFundingWithdrawalStatus
{
    /// <summary>
    /// Withdrawal ID
    /// When retrieving deposit status, wdId returns blank "".
    /// </summary>
    [JsonProperty("wdId")]
    public string WithdrawalId { get; set; }

    /// <summary>
    /// The detailed stage and status of the deposit/withdrawal
    /// The message in front of the colon is the stage; the message after the colon is the ongoing status.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; }

    /// <summary>
    /// Estimated complete time
    /// The timezone is UTC+8. The format is MM/dd/yyyy, h:mm:ss AM/PM
    /// estCompleteTime is only an approximate estimated time, for reference only.
    /// </summary>
    [JsonProperty("estCompleteTime")]
    public string EstimatedCompleteTime { get; set; }
}