#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Economic calendar query request
/// </summary>
public record OkxPublicEconomicCalendarRequest
{
    public string? Region { get; set; }
    public OkxPublicEventImportance? Importance { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
