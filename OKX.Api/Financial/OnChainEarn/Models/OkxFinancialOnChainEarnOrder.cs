namespace OKX.Api.Financial.OnChainEarn;

/// <summary>
/// OKX Financial On Chain Earn Order
/// </summary>
public class OkxFinancialOnChainEarnOrder
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    /// <summary>
    /// Order state
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxFinancialOnChainEarnOrderStateConverter))]
    public OkxFinancialOnChainEarnOrderState State { get; set; }

    /// <summary>
    /// Protocol
    /// </summary>
    [JsonProperty("protocol")]
    public string Protocol { get; set; }

    /// <summary>
    /// Protocol type
    /// defi: on-chain earn
    /// </summary>
    [JsonProperty("protocolType")]
    public string ProtocolType { get; set; }

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
    /// Order purchased time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("purchasedTime")]
    public long? PurchasedTimestamp { get; set; }

    /// <summary>
    /// Order purchased time
    /// </summary>
    [JsonIgnore]
    public DateTime? PurchasedTime { get { return PurchasedTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Estimated redemption settlement time
    /// </summary>
    [JsonProperty("estSettlementTime")]
    public long? EstimatedSettlementTimestamp { get; set; }

    /// <summary>
    /// Estimated redemption settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime? EstimatedSettlementTime { get { return EstimatedSettlementTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Deadline for cancellation of redemption application
    /// </summary>
    [JsonProperty("cancelRedemptionDeadline")]
    public long? CancelRedemptionDeadlineTimestamp { get; set; }

    /// <summary>
    /// Deadline for cancellation of redemption application
    /// </summary>
    [JsonIgnore]
    public DateTime? CancelRedemptionDeadlineTime { get { return CancelRedemptionDeadlineTimestamp?.ConvertFromMilliseconds(); } }
}