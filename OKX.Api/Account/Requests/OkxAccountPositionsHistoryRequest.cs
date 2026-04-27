#pragma warning disable CS1591
namespace OKX.Api.Account;

/// <summary>
/// Positions history query request
/// </summary>
public record OkxAccountPositionsHistoryRequest
{
    public OkxInstrumentType? InstrumentType { get; set; }
    public string? InstrumentId { get; set; }
    public OkxAccountMarginMode? MarginMode { get; set; }
    public OkxClosingPositionType? Type { get; set; }
    public string? PositionId { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
