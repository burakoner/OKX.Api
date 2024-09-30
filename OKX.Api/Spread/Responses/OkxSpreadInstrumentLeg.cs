namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Instrument Leg
/// </summary>
public record OkxSpreadInstrumentLeg
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// The direction of the leg of the spread. Valid Values include buy and sell.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }
}