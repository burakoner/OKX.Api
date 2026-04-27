#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Delivery or exercise history query request
/// </summary>
public record OkxPublicDeliveryExerciseHistoryRequest
{
    public OkxInstrumentType InstrumentType { get; set; }
    public string? InstrumentFamily { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
