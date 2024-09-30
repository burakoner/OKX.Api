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

    //[JsonProperty("atRiskIdx")]
    //public object[] AtRiskIndex { get; set; }

    //[JsonProperty("atRiskMgn")]
    //public object[] AtRiskMargin { get; set; }

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
