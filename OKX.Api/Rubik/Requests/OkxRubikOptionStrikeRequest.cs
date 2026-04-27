#pragma warning disable CS1591
namespace OKX.Api.Rubik;

/// <summary>
/// Option strike statistics request
/// </summary>
public record OkxRubikOptionStrikeRequest
{
    public string? Currency { get; set; }
    public string? ExpiryTime { get; set; }
    public string? Period { get; set; }
}
#pragma warning restore CS1591
