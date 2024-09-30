namespace OKX.Api.Account;

/// <summary>
/// VIP Interest Accrued Data
/// </summary>
public class OkxAccountVipInterestAccruedData
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Interest Rate
    /// </summary>
    [JsonProperty("interestRate")]
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Liability
    /// </summary>
    [JsonProperty("liab")]
    public decimal Liability { get; set; }

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
}
