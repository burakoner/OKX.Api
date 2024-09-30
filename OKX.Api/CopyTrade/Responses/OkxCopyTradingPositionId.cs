namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxLeadingPositionId
/// </summary>
public class OkxCopyTradingPositionId
{
    /// <summary>
    /// Leading position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long PositionId { get; set; }
}
