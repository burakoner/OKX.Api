namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Suborder State
/// </summary>
public enum OkxSignalBotSuborderState
{
    /// <summary>
    /// Live
    /// </summary>
    Live,

    /// <summary>
    /// Partially Filled
    /// </summary>
    PartiallyFilled,

    /// <summary>
    /// Filled
    /// </summary>
    Filled,

    /// <summary>
    /// Cancelling
    /// </summary>
    Cancelling,

    /// <summary>
    /// Cancelled
    /// </summary>
    Cancelled,
}
