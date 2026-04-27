#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Insurance fund query request
/// </summary>
public record OkxPublicInsuranceFundQueryRequest
{
    public OkxInstrumentType InstrumentType { get; set; }
    public OkxPublicInsuranceType? Type { get; set; }
    public string? InstrumentFamily { get; set; }
    public string? Currency { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
