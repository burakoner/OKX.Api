#pragma warning disable CS1591
namespace OKX.Api.Rubik;

/// <summary>
/// Contract taker volume request
/// </summary>
public record OkxRubikContractTakerVolumeRequest
{
    public string? InstrumentId { get; set; }
    public string? Period { get; set; }
    public string? Unit { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
