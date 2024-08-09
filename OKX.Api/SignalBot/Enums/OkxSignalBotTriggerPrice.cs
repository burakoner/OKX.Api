namespace OKX.Api.SignalBot.Enums;

/// <summary>
/// Type of set the take-profit and stop-loss trigger price
/// </summary>
public enum OkxSignalBotTriggerPrice
{
    /// <summary>
    /// Based on the estimated profit and loss percentage from the entry point
    /// </summary>
    ProfitAndLossPercentage,

    /// <summary>
    /// Based on price increase or decrease from the crypto’s entry price
    /// </summary>
    Price,
}
