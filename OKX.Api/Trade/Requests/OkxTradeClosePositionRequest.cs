#pragma warning disable CS1591
namespace OKX.Api.Trade;

/// <summary>
/// Close position request
/// </summary>
public record OkxTradeClosePositionRequest
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    public string? InstrumentId { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    public OkxAccountMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    public OkxTradePositionSide? PositionSide { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Auto cancel pending close orders
    /// </summary>
    public bool? AutoCancel { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    public string? ClientOrderId { get; set; }
}
#pragma warning restore CS1591
