#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Market data history query request
/// </summary>
public record OkxPublicMarketDataHistoryQueryRequest
{
    public OkxPublicMarketDataHistoryModule Module { get; set; }
    public OkxInstrumentType InstrumentType { get; set; }
    public OkxPublicDateAggregationType DateAggregationType { get; set; }
    public string? InstrumentIdList { get; set; }
    public string? InstrumentFamilyList { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
}
#pragma warning restore CS1591
