namespace OKX.Api.Funding;

/// <summary>
/// OKX Deposit History
/// </summary>
public record OkxFundingDepositHistory
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Chain
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// From
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Area Code From
    /// </summary>
    [JsonProperty("areaCodeFrom")]
    public string AreaCodeFrom { get; set; } = string.Empty;

    /// <summary>
    /// To
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Transaction ID
    /// </summary>
    [JsonProperty("txId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxFundingDepositState State { get; set; }

    /// <summary>
    /// Deposit ID
    /// </summary>
    [JsonProperty("depId")]
    public string DepositId { get; set; } = string.Empty;

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
