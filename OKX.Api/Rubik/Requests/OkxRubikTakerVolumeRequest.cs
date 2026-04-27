#pragma warning disable CS1591
namespace OKX.Api.Rubik;

/// <summary>
/// Taker volume request
/// </summary>
public record OkxRubikTakerVolumeRequest
{
    public string? Currency { get; set; }
    public OkxInstrumentType InstrumentType { get; set; }
    public string? Period { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
}
#pragma warning restore CS1591
