namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan maximum loan amount response
/// </summary>
public record OkxFinancialFlexibleLoanMaximumLoan
{
    /// <summary>
    /// Borrow currency
    /// </summary>
    [JsonProperty("borrowCcy")]
    public string BorrowCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Maximum loan amount
    /// </summary>
    [JsonProperty("maxLoan"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal MaximumLoanAmount { get; set; }

    /// <summary>
    /// Notional value in USD
    /// </summary>
    [JsonProperty("notionalUsd"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal NotionalUsd { get; set; }

    /// <summary>
    /// Remaining quota
    /// </summary>
    [JsonProperty("remainingQuota"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal RemainingQuota { get; set; }
}
