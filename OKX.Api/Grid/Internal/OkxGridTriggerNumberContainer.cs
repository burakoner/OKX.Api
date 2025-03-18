namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Trigger Number
/// </summary>
internal record OkxGridTriggerNumberContainer
{
    /// <summary>
    /// Trigger Number
    /// </summary>
    [JsonProperty("triggerNum")]
    public long? Payload { get; set; }
}