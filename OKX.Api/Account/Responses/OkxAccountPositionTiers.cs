namespace OKX.Api.Account;

/// <summary>
/// OkxPosition
/// </summary>
public class OkxAccountPositionTiers
{
    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Maximum Positions
    /// </summary>
    [JsonProperty("maxSz")]
    public long MaximumPositions { get; set; }

    /// <summary>
    /// Position Type
    /// </summary>
    [JsonProperty("posType")]
    public OkxTradePositionType PositionType { get; set; }
}