namespace OKX.Api.SignalBot;

/// <summary>
/// Okx Signal Entry Type
/// </summary>
public enum OkxSignalBotEntryType
{
    /// <summary>
    /// TradingView Signal
    /// </summary>
    [Map("1")]
    TradingViewSignal,

    /// <summary>
    /// Fixed margin
    /// </summary>
    [Map("2")]
    FixedMargin,

    /// <summary>
    /// Contracts
    /// </summary>
    [Map("3")]
    Contracts,

    /// <summary>
    /// Percentage of free margin
    /// </summary>
    [Map("4")]
    PercentageOfFreeMargin,

    /// <summary>
    /// Percentage of the initial invested margin
    /// </summary>
    [Map("5")]
    PercentageOfTheInitialInvestedMargin
}
