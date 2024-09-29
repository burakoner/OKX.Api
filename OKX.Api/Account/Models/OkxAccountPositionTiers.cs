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
    public string Underlying { get; set; } = "";

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = "";

    /// <summary>
    /// Maximum Positions
    /// </summary>
    [JsonProperty("maxSz")]
    public long MaximumPositions { get; set; }

    /// <summary>
    /// Position Type
    /// </summary>
    [JsonProperty("posType"), JsonConverter(typeof(OkxTradePositionTypeConverter))]
    public OkxTradePositionType PositionType { get; set; }
}