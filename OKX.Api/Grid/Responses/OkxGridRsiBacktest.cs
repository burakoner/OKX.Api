namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid RSI Backtest
/// </summary>
public class OkxGridRsiBacktest
{
    /// <summary>
    /// Trigger number
    /// </summary>
    [JsonProperty("triggerNum")]
    public string TriggerNumber { get; set; } = string.Empty;
}
