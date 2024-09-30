namespace OKX.Api.Account;

/// <summary>
/// Okx VIP Loan Order
/// </summary>
public record OkxAccountVipLoanOrderDetails
{
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
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountVipLoanType Type { get; set; }
    
    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Failure reason
    /// </summary>
    [JsonProperty("failReason")]
    public string FailReason { get; set; } = string.Empty;
}
