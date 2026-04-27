#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Funding transfer state query request
/// </summary>
public record OkxFundingTransferStateRequest
{
    public long? TransferId { get; set; }
    public string? ClientOrderId { get; set; }
    public OkxFundingTransferType? Type { get; set; }
}
#pragma warning restore CS1591
