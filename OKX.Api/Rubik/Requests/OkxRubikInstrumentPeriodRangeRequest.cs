#pragma warning disable CS1591
namespace OKX.Api.Rubik;

/// <summary>
/// Instrument based statistics request
/// </summary>
public record OkxRubikInstrumentPeriodRangeRequest
{
    public string? InstrumentId { get; set; }
    public string? Period { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
