#pragma warning disable CS1591
namespace OKX.Api.Rubik;

/// <summary>
/// Currency based statistics request
/// </summary>
public record OkxRubikCurrencyPeriodRangeRequest
{
    public string? Currency { get; set; }
    public string? Period { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
}
#pragma warning restore CS1591
