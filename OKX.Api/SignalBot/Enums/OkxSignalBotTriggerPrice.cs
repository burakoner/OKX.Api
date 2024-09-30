namespace OKX.Api.SignalBot;

/// <summary>
/// Type of set the take-profit and stop-loss trigger price
/// </summary>
public enum OkxSignalBotTriggerPrice
{
    /// <summary>
    /// Based on the estimated profit and loss percentage from the entry point
    /// </summary>
    [Map("pnl")]
    ProfitAndLossPercentage,

    /// <summary>
    /// Based on price increase or decrease from the crypto’s entry price
    /// </summary>
    [Map("price")]
    Price,
}
