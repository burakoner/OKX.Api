namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Suborder State
/// </summary>
public enum OkxSignalBotSuborderState : byte
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Partially Filled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled = 2,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 3,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("cancelling")]
    Cancelling = 4,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled = 5,
}
