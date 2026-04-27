#pragma warning disable CS1591
namespace OKX.Api.Trade;

/// <summary>
/// Order precheck request
/// </summary>
public record OkxTradeOrderPrecheckRequest
{
    public string? InstrumentId { get; set; }
    public OkxTradeMode? TradeMode { get; set; }
    public OkxTradeOrderSide? OrderSide { get; set; }
    public OkxTradeOrderType? OrderType { get; set; }
    public decimal? Size { get; set; }
    public decimal? Price { get; set; }
    public bool? ReduceOnly { get; set; }
    public OkxTradePositionSide? PositionSide { get; set; }
    public OkxTradeQuantityType? QuantityType { get; set; }
    public OkxTradeEventOutcome? Outcome { get; set; }
    public IEnumerable<OkxTradeOrderPlaceRequestAttachedAlgo>? AttachedAlgoOrders { get; set; }
}
#pragma warning restore CS1591
