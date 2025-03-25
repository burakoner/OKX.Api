namespace OKX.Api.Funding;

/// <summary>
/// OKX Deposit Update
/// </summary>
public record OkxFundingDepositUpdate
{
    /// <summary>
    /// User Identifier of the message producer
    /// </summary>
    [JsonProperty("uid")]
    public string UID { get; set; } = string.Empty;

    /// <summary>
    /// Sub-account name
    /// If the message producer is master account, the parameter will return ""
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccount { get; set; } = string.Empty;

    /// <summary>
    /// Push time, the millisecond format of the Unix timestamp, e.g. 1597026383085
    /// </summary>
    [JsonProperty("pTime")]
    public long PushTimestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime PushTime => PushTimestamp.ConvertFromMilliseconds();

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
    public string FromWithdrawalId { get; set; } = string.Empty;

    /// <summary>
    /// Actual Deposit Blockchain Confirm
    /// </summary>
    [JsonProperty("actualDepBlkConfirm")]
    public int? ActualDepositBlockchainConfirm { get; set; }
}
