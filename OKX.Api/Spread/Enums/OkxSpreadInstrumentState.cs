namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread State
/// </summary>
public enum OkxSpreadInstrumentState
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Suspended
    /// </summary>
    [Map("suspend")]
    Suspended = 2,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired = 3,
}