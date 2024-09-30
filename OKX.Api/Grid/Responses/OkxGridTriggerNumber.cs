namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Trigger Number
/// </summary>
public record OkxGridTriggerNumber
{
    /// <summary>
    /// Trigger Number
    /// </summary>
    [JsonProperty("triggerNum")]
    public int TriggerNumber { get; set; }
}