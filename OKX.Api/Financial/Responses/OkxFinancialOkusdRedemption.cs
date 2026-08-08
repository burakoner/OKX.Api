namespace OKX.Api.Financial;

/// <summary>
/// OKUSD Redemption
/// </summary>
public record OkxFinancialOkusdRedemption
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
    /// Redemption currency, always OKUSD
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// OKUSD amount redeemed
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Fee charged in USDT, truncated to 8 decimal places by OKX
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Net USDT amount credited, truncated to 8 decimal places by OKX
    /// </summary>
    [JsonProperty("usdtAmt")]
    public decimal UsdtAmount { get; set; }

    /// <summary>
    /// Redemption type
    /// </summary>
    [JsonProperty("redeemType")]
    public OkxFinancialOkusdRedemptionType RedemptionType { get; set; }

    /// <summary>
    /// Order state
    /// </summary>
    [JsonProperty("state")]
    public OkxFinancialOkusdRedemptionState State { get; set; }

    /// <summary>
    /// Estimated settlement timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("estSettlementTime")]
    public long EstimatedSettlementTimestamp { get; set; }

    /// <summary>
    /// Estimated settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime EstimatedSettlementTime => EstimatedSettlementTimestamp.ConvertFromMilliseconds();

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