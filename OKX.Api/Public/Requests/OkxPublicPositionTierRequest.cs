#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Position tiers query request
/// </summary>
public record OkxPublicPositionTierRequest
{
    public OkxInstrumentType InstrumentType { get; set; }
    public OkxAccountMarginMode MarginMode { get; set; }
    public string? InstrumentId { get; set; }
    public string? InstrumentFamily { get; set; }
    public string? Currency { get; set; }
    public string? Tier { get; set; }
}
#pragma warning restore CS1591
