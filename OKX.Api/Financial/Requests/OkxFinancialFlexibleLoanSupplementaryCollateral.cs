namespace OKX.Api.Financial;

/// <summary>
/// Supplementary collateral asset for flexible loan calculations
/// </summary>
public record OkxFinancialFlexibleLoanSupplementaryCollateral
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    public decimal Amount { get; set; }
}
