#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Funding rate history query request
/// </summary>
public record OkxPublicFundingRateHistoryRequest
{
    public string? InstrumentId { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
