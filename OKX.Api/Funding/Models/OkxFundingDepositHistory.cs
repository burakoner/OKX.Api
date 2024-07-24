using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Deposit History
/// </summary>
public class OkxFundingDepositHistory
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Chain
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// From
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    /// Area Code From
    /// </summary>
    [JsonProperty("areaCodeFrom")]
    public string AreaCodeFrom { get; set; }

    /// <summary>
    /// To
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    /// Transaction ID
    /// </summary>
    [JsonProperty("txId")]
    public string TransactionId { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxFundingDepositStateConverter))]
    public OkxFundingDepositState State { get; set; }

    /// <summary>
    /// Deposit ID
    /// </summary>
    [JsonProperty("depId")]
    public string DepositId { get; set; }

    /// <summary>
    /// From Withdrawal ID
    /// </summary>
    [JsonProperty("fromWdId")]
    public long? FromWithdrawalId { get; set; }

    /// <summary>
    /// Actual Deposit Blockchain Confirm
    /// </summary>
    [JsonProperty("actualDepBlkConfirm")]
    public decimal? ActualDepositBlockchainConfirm { get; set; }
}
