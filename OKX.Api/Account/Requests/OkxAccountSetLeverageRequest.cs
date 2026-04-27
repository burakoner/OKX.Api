#pragma warning disable CS1591
namespace OKX.Api.Account;

/// <summary>
/// Set leverage request
/// </summary>
public record OkxAccountSetLeverageRequest
{
    public decimal? Leverage { get; set; }
    public string? Currency { get; set; }
    public string? InstrumentId { get; set; }
    public OkxAccountMarginMode? MarginMode { get; set; }
    public OkxTradePositionSide? PositionSide { get; set; }
}
#pragma warning restore CS1591
