#pragma warning disable CS1591
namespace OKX.Api.Spread;

/// <summary>
/// Spread instruments query request
/// </summary>
public record OkxSpreadInstrumentQueryRequest
{
    public string? BaseCurrency { get; set; }
    public string? InstrumentId { get; set; }
    public string? SpreadId { get; set; }
    public OkxSpreadInstrumentState? State { get; set; }
}
#pragma warning restore CS1591
