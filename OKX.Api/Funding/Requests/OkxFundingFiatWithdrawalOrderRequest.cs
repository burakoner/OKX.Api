#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Fiat withdrawal order request
/// </summary>
public record OkxFundingFiatWithdrawalOrderRequest
{
    public string? PaymentAccountId { get; set; }
    public string? Currency { get; set; }
    public decimal Amount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? ClientOrderId { get; set; }
}
#pragma warning restore CS1591
