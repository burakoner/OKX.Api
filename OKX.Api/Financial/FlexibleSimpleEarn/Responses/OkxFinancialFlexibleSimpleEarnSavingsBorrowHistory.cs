namespace OKX.Api.Financial.FlexibleSimpleEarn;

/// <summary>
/// OKX Flexible Simple Earn Savings Borrow History
/// </summary>
public record OkxFinancialFlexibleSimpleEarnSavingsBorrowHistory
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
    /// Lending annual interest rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }
    
    /// <summary>
    /// Time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
