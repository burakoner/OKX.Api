#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Public instruments query request
/// </summary>
public record OkxPublicInstrumentQueryRequest
{
    public OkxInstrumentType InstrumentType { get; set; }
    public string? InstrumentId { get; set; }
    public string? InstrumentFamily { get; set; }
    public string? SeriesId { get; set; }
    public bool Signed { get; set; }
}
#pragma warning restore CS1591
