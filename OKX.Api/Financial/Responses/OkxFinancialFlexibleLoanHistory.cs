namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan history record
/// </summary>
public record OkxFinancialFlexibleLoanHistory
{
    /// <summary>
    /// Reference ID
    /// </summary>
    [JsonProperty("refId")]
    public string ReferenceId { get; set; } = string.Empty;

    /// <summary>
    /// History type
    /// </summary>
    [JsonProperty("type")]
    public OkxFinancialFlexibleLoanHistoryType Type { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Amount { get; set; }

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
