namespace OKX.Api.Funding;

/// <summary>
/// OKX Funding Bill
/// </summary>
public record OkxFundingBill
{
    /// <summary>
    /// Bill ID
    /// </summary>
    [JsonProperty("billId")]
    public long BillId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Client Order ID
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;
    
    /// <summary>
    /// Balance Change
    /// </summary>
    [JsonProperty("balChg")]
    public decimal BalanceChange { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxFundingBillType Type { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
