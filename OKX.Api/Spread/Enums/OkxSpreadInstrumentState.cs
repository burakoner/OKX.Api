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
    Live,

    /// <summary>
    /// Suspended
    /// </summary>
    [Map("suspend")]
    Suspended,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired,
}