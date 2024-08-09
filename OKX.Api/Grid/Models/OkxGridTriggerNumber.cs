namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid Trigger Number
/// </summary>
public class OkxGridTriggerNumber
{
    /// <summary>
    /// Trigger Number
    /// </summary>
    [JsonProperty("triggerNum")]
    public int TriggerNumber { get; set; }
}