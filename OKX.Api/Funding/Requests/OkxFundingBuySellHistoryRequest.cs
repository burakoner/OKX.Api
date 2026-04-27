#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Buy/sell history query request
/// </summary>
public record OkxFundingBuySellHistoryRequest
{
    public string? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
    public string? State { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
