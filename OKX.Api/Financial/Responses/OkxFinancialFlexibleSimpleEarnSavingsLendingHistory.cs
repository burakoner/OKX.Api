namespace OKX.Api.Financial;

/// <summary>
/// OKX Flexible Simple Earn Savings Lending History
/// </summary>
public record OkxFinancialFlexibleSimpleEarnSavingsLendingHistory
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Lending amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency earnings
    /// </summary>
    [JsonProperty("earnings")]
    public decimal Earnings { get; set; }

    /// <summary>
    /// Lending annual interest rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Lending time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Lending time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
