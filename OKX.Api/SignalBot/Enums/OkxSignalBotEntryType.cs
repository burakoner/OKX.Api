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
    TradingViewSignal = 1,

    /// <summary>
    /// Fixed margin
    /// </summary>
    [Map("2")]
    FixedMargin = 2,

    /// <summary>
    /// Contracts
    /// </summary>
    [Map("3")]
    Contracts = 3,

    /// <summary>
    /// Percentage of free margin
    /// </summary>
    [Map("4")]
    PercentageOfFreeMargin = 4,

    /// <summary>
    /// Percentage of the initial invested margin
    /// </summary>
    [Map("5")]
    PercentageOfTheInitialInvestedMargin = 5
}
