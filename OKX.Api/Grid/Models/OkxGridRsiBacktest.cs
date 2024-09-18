namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid RSI Backtest
/// </summary>
public class OkxGridRsiBacktest
{
    /// <summary>
    /// Trigger number
    /// </summary>
    [JsonProperty("triggerNum")]
    public string TriggerNumber { get; set; }
}
