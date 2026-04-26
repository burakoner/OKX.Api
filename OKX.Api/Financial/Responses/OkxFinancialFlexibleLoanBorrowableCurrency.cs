namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan borrowable currency
/// </summary>
public record OkxFinancialFlexibleLoanBorrowableCurrency
{
    /// <summary>
    /// Borrow currency
    /// </summary>
    [JsonProperty("borrowCcy")]
    public string BorrowCurrency { get; set; } = string.Empty;
}
