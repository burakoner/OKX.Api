namespace OKX.Api.SignalBot;

/// <summary>
/// Type of set the take-profit and stop-loss trigger price
/// </summary>
public enum OkxSignalBotTriggerPrice : byte
{
    /// <summary>
    /// Based on the estimated profit and loss percentage from the entry point
    /// </summary>
    [Map("pnl")]
    PnlPercent = 1,

    /// <summary>
    /// Based on price increase or decrease from the crypto’s entry price
    /// </summary>
    [Map("price")]
    Price = 2,
}
