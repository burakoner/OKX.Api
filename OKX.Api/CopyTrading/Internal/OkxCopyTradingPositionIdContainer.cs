namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxLeadingPositionId
/// </summary>
internal record OkxCopyTradingPositionIdContainer
{
    /// <summary>
    /// Leading position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long? Payload { get; set; }
}
