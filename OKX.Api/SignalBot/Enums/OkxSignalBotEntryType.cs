namespace OKX.Api.SignalBot.Enums;

/// <summary>
/// Okx Signal Entry Type
/// </summary>
public enum OkxSignalBotEntryType
{
    /// <summary>
    /// TradingView Signal
    /// </summary>
    TradingViewSignal,

    /// <summary>
    /// Fixed margin
    /// </summary>
    FixedMargin,

    /// <summary>
    /// Contracts
    /// </summary>
    Contracts,

    /// <summary>
    /// Percentage of free margin
    /// </summary>
    PercentageOfFreeMargin,

    /// <summary>
    /// Percentage of the initial invested margin
    /// </summary>
    PercentageOfTheInitialInvestedMargin
}
