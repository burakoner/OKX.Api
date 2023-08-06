namespace OKX.Api.Models.CopyTrading;

/// <summary>
/// OkxLeadingPositionId
/// </summary>
public class OkxLeadingPositionId
{
    /// <summary>
    /// Leading position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long PositionId { get; set; }
}
