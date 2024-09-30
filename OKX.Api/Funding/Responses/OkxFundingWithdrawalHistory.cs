namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal History
/// </summary>
public record OkxFundingWithdrawalHistory
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
    /// Non Tradable Asset
    /// </summary>
    [JsonProperty("nonTradableAsset")]
    public bool? NonTradableAsset { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

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
    /// Area Code To
    /// </summary>
    [JsonProperty("areaCodeTo")]
    public string AreaCodeTo { get; set; } = string.Empty;

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Payment Id
    /// </summary>
    [JsonProperty("pmtId")]
    public string PaymentId { get; set; } = string.Empty;

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; } = string.Empty;

    //[JsonProperty("addrEx")]
    //public addrEx addrEx { get; set; }

    /// <summary>
    /// Transaction Id
    /// </summary>
    [JsonProperty("txId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Fee Currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = string.Empty;

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxFundingWithdrawalState State { get; set; }

    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;
}
