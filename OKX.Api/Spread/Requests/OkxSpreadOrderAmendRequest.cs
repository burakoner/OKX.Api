#pragma warning disable CS1591
namespace OKX.Api.Spread;

/// <summary>
/// Spread amend order request
/// </summary>
public record OkxSpreadRestOrderAmendRequest
{
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
    public string? RequestId { get; set; }
    public decimal? NewQuantity { get; set; }
    public decimal? NewPrice { get; set; }
}
#pragma warning restore CS1591
