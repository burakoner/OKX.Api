#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Open interest query request
/// </summary>
public record OkxPublicOpenInterestRequest
{
    public OkxInstrumentType InstrumentType { get; set; }
    public string? InstrumentId { get; set; }
    public string? InstrumentFamily { get; set; }
}
#pragma warning restore CS1591
