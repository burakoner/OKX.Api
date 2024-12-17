namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial On Chain Earn Order
/// </summary>
public record OkxFinancialOnChainEarnOrder
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("productId")]
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    /// Order state
    /// </summary>
    [JsonProperty("state")]
    public OkxFinancialOnChainEarnOrderState State { get; set; }

    /// <summary>
    /// Protocol
    /// </summary>
    [JsonProperty("protocol")]
    public string Protocol { get; set; } = string.Empty;

    /// <summary>
    /// Protocol type
    /// defi: on-chain earn
    /// </summary>
    [JsonProperty("protocolType")]
    public string ProtocolType { get; set; } = string.Empty;

    /// <summary>
    /// Protocol term
    /// It will return the days of fixed term and will return 0 for flexible product
    /// </summary>
    [JsonProperty("term")]
    public int Term { get; set; }

    /// <summary>
    /// Estimated annualization
    /// If the annualization is 7% , this field is 0.07
    /// </summary>
    [JsonProperty("apy")]
    public decimal APY { get; set; }

    /// <summary>
    /// Investment data
    /// </summary>
    [JsonProperty("investData")]
    public List<OkxFinancialOnChainEarnInvestData> InvestData { get; set; } = [];

    /// <summary>
    /// Earning data
    /// </summary>
    [JsonProperty("earningData")]
    public List<OkxFinancialOnChainEarnEarningData> EarningData { get; set; } = [];

    /// <summary>
    /// Fast redemption data
    /// </summary>
    [JsonProperty("fastRedemptionData")]
    public List<OkxFinancialOnChainEarnFastRedemptionData> FastRedemptionData { get; set; } = [];

    /// <summary>
    /// Order purchased time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("purchasedTime")]
    public long? PurchasedTimestamp { get; set; }

    /// <summary>
    /// Order purchased time
    /// </summary>
    [JsonIgnore]
    public DateTime? PurchasedTime => PurchasedTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Estimated redemption settlement time
    /// </summary>
    [JsonProperty("estSettlementTime")]
    public long? EstimatedSettlementTimestamp { get; set; }

    /// <summary>
    /// Estimated redemption settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime? EstimatedSettlementTime => EstimatedSettlementTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Deadline for cancellation of redemption application
    /// </summary>
    [JsonProperty("cancelRedemptionDeadline")]
    public long? CancelRedemptionDeadlineTimestamp { get; set; }

    /// <summary>
    /// Deadline for cancellation of redemption application
    /// </summary>
    [JsonIgnore]
    public DateTime? CancelRedemptionDeadlineTime => CancelRedemptionDeadlineTimestamp?.ConvertFromMilliseconds();
}