#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Unit conversion query request
/// </summary>
public record OkxPublicUnitConvertRequest
{
    public string? InstrumentId { get; set; }
    public decimal Size { get; set; }
    public decimal? Price { get; set; }
    public OkxPublicConvertType? Type { get; set; }
    public OkxPublicConvertUnit? Unit { get; set; }
    public OkxPublicConvertOperation? OperationType { get; set; }
}
#pragma warning restore CS1591
