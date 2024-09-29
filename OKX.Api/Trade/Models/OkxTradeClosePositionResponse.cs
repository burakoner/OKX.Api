namespace OKX.Api.Trade;

/// <summary>
/// OKX Close Position Response
/// </summary>
public class OkxTradeClosePositionResponse
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Position Side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Client-supplied ID
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }
}
