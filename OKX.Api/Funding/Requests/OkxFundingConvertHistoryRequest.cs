#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Convert history query request
/// </summary>
public record OkxFundingConvertHistoryRequest
{
    public string? ClientOrderId { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
    public string? Tag { get; set; }
}
#pragma warning restore CS1591
