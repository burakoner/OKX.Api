namespace OKX.Api.Funding;

/// <summary>
/// OKX fiat order
/// </summary>
public record OkxFundingFiatOrder
{
    /// <summary>
    /// Order creation timestamp in milliseconds.
    /// </summary>
    [JsonProperty("cTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? CreateTimestamp { get; set; }

    /// <summary>
    /// Order creation time.
    /// </summary>
    [JsonIgnore]
    public DateTime? CreateTime => CreateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Last update timestamp in milliseconds.
    /// </summary>
    [JsonProperty("uTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Last update time.
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Fiat order identifier.
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Payment method used for the fiat order.
    /// </summary>
    [JsonProperty("paymentMethod")]
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Payment account identifier used for the fiat order.
    /// </summary>
    [JsonProperty("paymentAcctId")]
    public string PaymentAccountId { get; set; } = string.Empty;

    /// <summary>
    /// Fiat fee charged for the order.
    /// </summary>
    [JsonProperty("fee")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Fiat amount of the order.
    /// </summary>
    [JsonProperty("amt")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Fiat currency of the order.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Order state reported by OKX.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Client supplied identifier for the fiat order.
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;
}
