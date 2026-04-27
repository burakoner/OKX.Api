#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Funding bill query request
/// </summary>
public record OkxFundingBillQueryRequest
{
    public string? Currency { get; set; }
    public OkxFundingBillType? Type { get; set; }
    public string? ClientOrderId { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
    public int PagingType { get; set; } = 1;
}
#pragma warning restore CS1591
