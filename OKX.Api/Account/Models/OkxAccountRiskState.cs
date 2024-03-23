namespace OKX.Api.Account.Models;

/// <summary>
/// OKX Account Risk State
/// </summary>
public class OkxAccountRiskState
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// At Risk
    /// </summary>
    [JsonProperty("atRisk")]
    public bool AtRisk { get; set; }

    //[JsonProperty("atRiskIdx")]
    //public object[] AtRiskIndex { get; set; }

    //[JsonProperty("atRiskMgn")]
    //public object[] AtRiskMargin { get; set; }
}
