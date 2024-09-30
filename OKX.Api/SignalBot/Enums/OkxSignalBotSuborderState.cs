namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Suborder State
/// </summary>
public enum OkxSignalBotSuborderState
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live,

    /// <summary>
    /// Partially Filled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("cancelling")]
    Cancelling,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled,
}
