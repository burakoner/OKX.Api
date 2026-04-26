namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan accrued interest record
/// </summary>
public record OkxFinancialFlexibleLoanAccruedInterest
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Accrued interest
    /// </summary>
    [JsonProperty("interest"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Interest { get; set; }

    /// <summary>
    /// Interest rate
    /// </summary>
    [JsonProperty("interestRate"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Loan amount
    /// </summary>
    [JsonProperty("loan"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal LoanAmount { get; set; }

    /// <summary>
    /// Reference ID
    /// </summary>
    [JsonProperty("refId")]
    public string ReferenceId { get; set; } = string.Empty;

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
