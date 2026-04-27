#pragma warning disable CS1591
namespace OKX.Api.Rubik;

/// <summary>
/// Option statistics request
/// </summary>
public record OkxRubikOptionPeriodRequest
{
    public string? Currency { get; set; }
    public string? Period { get; set; }
}
#pragma warning restore CS1591
