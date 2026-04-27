#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Funding transfer request
/// </summary>
public record OkxFundingTransferRequest
{
    public OkxFundingTransferType Type { get; set; }
    public string? Currency { get; set; }
    public decimal Amount { get; set; }
    public OkxAccount FromAccount { get; set; }
    public OkxAccount ToAccount { get; set; }
    public string? SubAccountName { get; set; }
    public bool? LoanTransfer { get; set; }
    public bool? OmitPositionRisk { get; set; }
    public string? ClientOrderId { get; set; }
}
#pragma warning restore CS1591
