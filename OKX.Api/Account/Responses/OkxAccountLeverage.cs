namespace OKX.Api.Account;

/// <summary>
/// OkxLeverage
/// </summary>
public record OkxAccountLeverage
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode")]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }
}
