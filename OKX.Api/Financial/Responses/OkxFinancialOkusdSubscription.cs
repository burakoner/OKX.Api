namespace OKX.Api.Financial;

/// <summary>
/// OKUSD Subscription
/// </summary>
public record OkxFinancialOkusdSubscription
{
    /// <summary>
    /// System-generated order ID
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Client-defined order ID
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Subscription currency, always USDT
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Actual USDT amount subscribed
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// OKUSD amount credited at a 1:1 rate
    /// </summary>
    [JsonProperty("okusdAmt")]
    public decimal OkusdAmount { get; set; }

    /// <summary>
    /// Order state
    /// </summary>
    [JsonProperty("state")]
    public OkxFinancialOkusdSubscriptionState State { get; set; }

    /// <summary>
    /// Order creation timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Order creation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}