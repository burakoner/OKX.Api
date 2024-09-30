namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Trigger Number
/// </summary>
internal record OkxGridTriggerNumber
{
    /// <summary>
    /// Trigger Number
    /// </summary>
    [JsonProperty("triggerNum")]
    public long Data { get; set; }
}