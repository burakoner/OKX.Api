namespace OKX.Api.Account;

/// <summary>
/// OKX Account Risk State
/// </summary>
public record OkxAccountRiskState
{
    /// <summary>
    /// At Risk
    /// </summary>
    [JsonProperty("atRisk")]
    public bool AtRisk { get; set; }

    /// <summary>
    /// derivatives risk unit list
    /// </summary>
    [JsonProperty("atRiskIdx")]
    public List<string> AtRiskIndex { get; set; } = [];

    /// <summary>
    /// margin risk unit list
    /// </summary>
    [JsonProperty("atRiskMgn")]
    public List<string> AtRiskMargin { get; set; } = [];

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
