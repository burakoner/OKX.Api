namespace OKX.Api.Account;

/// <summary>
/// Okx Position Builder Graph
/// </summary>
public record OkxAccountPositionBuilderGraph
{
    /// <summary>
    /// Graph type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Array of mmrData
    /// Return data in shockFactor ascending order
    /// </summary>
    [JsonProperty("mmrData")]
    public List<OkxAccountPositionBuilderGraphMmrData> MMRData { get; set; } = [];
}

/// <summary>
/// Okx Position Builder Graph MMR Data
/// </summary>
public class OkxAccountPositionBuilderGraphMmrData
{
    /// <summary>
    /// Price change ratio, data range -1 to 1.
    /// </summary>
    [JsonProperty("shockFactor")]
    public decimal ShockFactor { get; set; }

    /// <summary>
    /// Mmr at specific price
    /// </summary>
    [JsonProperty("mmr")]
    public decimal MMR { get; set; }

    /// <summary>
    /// Maintenance margin ratio at specific price
    /// </summary>
    [JsonProperty("mmrRatio")]
    public decimal MMRRatio { get; set; }
}
